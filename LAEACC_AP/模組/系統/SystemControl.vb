'::::::::::::::::::
'::: 系統控制   :::
'::: 物件控制   :::
'::: 系統加解密 :::
'::::::::::::::::::
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.IO
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Data.SqlClient


Module SystemControl
    '資料庫共用變數
    Dim objConSC As SqlConnection
    Dim objCmdSC As SqlCommand
    Dim objDRSC As SqlDataReader
    Dim strSQLSC As String
    Dim firstGreateGrid As String = "true"
    Public ReportDir As String = INI_Read("CONFIG", "SET", "ReportDir") 'DNS設定值

    Dim I As Integer '累進變數

    Public Sub MySystemControl()

    End Sub

#Region "系統控制"
    ''' <summary>
    ''' 關閉電腦
    ''' </summary>
    ''' <param name="strFuntion">關閉種類(空白：只關閉程式(預設) CS：同時關閉電腦)(可省略)</param>
    Sub ComputerShutdown(Optional strFuntion As String = "")
        '-i 顯示 GUI 介面，必須是第一個選項
        '-l 登出 (不能和 -m 選項一起使用)
        '-s 電腦關機
        '-r 關機並重新啟動電腦
        '-a 中止系統關機
        '-m \\\\computername 從遠端進行關機/重新啟動/中止
        '-t xx 將關機等候時間設定成 xx 秒
        '-c "comment" 關機註解 (最多 127 個字元)
        '-f 強制關閉執行中的應用程式，不顯示警告
        '-d [u][p]:xx:yy 關機原因代碼
        'u(是使用者代碼)
        'p(是預先計劃的關機代碼)
        'xx 是主要原因代碼 (小於 256 的正整數)
        'yy 是次要原因代碼 (小於 65536 的正整數)

        Select Case strFuntion
            Case "CS" '同時關閉電腦
                Shell("shutdown -s -f -t 0")
        End Select

        End '將軟體關閉
    End Sub
#End Region

#Region "物件控制"
#Region "輸入控制項的 ReadOnly 屬性及 Enabled 屬性"
    '主項目輸入項 的控制項屬性
    Sub Main_Control(ByVal objTextBox() As TextBox, ByVal objComboBox() As ComboBox, ByVal objRadioButton() As RadioButton, ByVal objDateTimePicker() As DateTimePicker, ByVal Status As Integer)
        Select Case Status
            Case 0 '一般模式
                'TextBox
                For I As Integer = 0 To objTextBox.Length - 1 Step 1
                    objTextBox(I).BackColor = Color.Ivory
                    objTextBox(I).ReadOnly = True
                Next

                'ComboBox
                For I As Integer = 0 To objComboBox.Length - 1 Step 1
                    objComboBox(I).Enabled = False
                Next

                'RadioButton
                For I As Integer = 0 To objRadioButton.Length - 1 Step 1
                    objRadioButton(I).Enabled = False
                Next

                'DateTimePicker
                For I As Integer = 0 To objDateTimePicker.Length - 1 Step 1
                    objDateTimePicker(I).Enabled = False
                Next

            Case 1 '新增模式 
                'TextBox
                For I As Integer = 0 To objTextBox.Length - 1 Step 1
                    objTextBox(I).BackColor = Color.White
                    objTextBox(I).ReadOnly = False
                    objTextBox(I).Text = ""
                Next

                'ComboBox
                For I As Integer = 0 To objComboBox.Length - 1 Step 1
                    objComboBox(I).Enabled = True
                    objComboBox(I).Text = ""
                Next

                'RadioButton
                For I As Integer = 0 To objRadioButton.Length - 1 Step 1
                    objRadioButton(I).Enabled = True
                    objRadioButton(I).Checked = False
                Next

                'DateTimePicker
                For I As Integer = 0 To objDateTimePicker.Length - 1 Step 1
                    objDateTimePicker(I).Enabled = True
                    objDateTimePicker(I).Value = Now
                Next

            Case 2 '修改模式
                'TextBox
                For I As Integer = 0 To objTextBox.Length - 1 Step 1
                    objTextBox(I).BackColor = Color.Ivory
                    objTextBox(I).ReadOnly = True
                Next

                'ComboBox
                For I As Integer = 0 To objComboBox.Length - 1 Step 1
                    objComboBox(I).Enabled = False
                Next

                'RadioButton
                For I As Integer = 0 To objRadioButton.Length - 1 Step 1
                    objRadioButton(I).Enabled = False
                Next

                'DateTimePicker
                For I As Integer = 0 To objDateTimePicker.Length - 1 Step 1
                    objDateTimePicker(I).Enabled = False
                Next

            Case 3 '複製模式
                'TextBox
                For I As Integer = 0 To objTextBox.Length - 1 Step 1
                    objTextBox(I).BackColor = Color.White
                    objTextBox(I).ReadOnly = False
                Next

                'ComboBox
                For I As Integer = 0 To objComboBox.Length - 1 Step 1
                    objComboBox(I).Enabled = True
                Next

                'RadioButton
                For I As Integer = 0 To objRadioButton.Length - 1 Step 1
                    objRadioButton(I).Enabled = True
                Next

                'DateTimePicker
                For I As Integer = 0 To objDateTimePicker.Length - 1 Step 1
                    objDateTimePicker(I).Enabled = True
                Next
        End Select
    End Sub

    '基本項目輸入項 的控制項屬性
    Sub Basic_Control(ByVal objTextBox() As TextBox, ByVal objComboBox() As ComboBox, ByVal objRadioButton() As RadioButton, ByVal objDateTimePicker() As DateTimePicker, ByVal Status As Integer)
        Select Case Status
            Case 0 '一般模式
                'TextBox
                For I As Integer = 0 To objTextBox.Length - 1 Step 1
                    objTextBox(I).BackColor = Color.Ivory
                    objTextBox(I).ReadOnly = True
                Next

                'ComboBox
                For I As Integer = 0 To objComboBox.Length - 1 Step 1
                    objComboBox(I).Enabled = False
                Next

                'RadioButton
                For I As Integer = 0 To objRadioButton.Length - 1 Step 1
                    objRadioButton(I).Enabled = False
                Next

                'DateTimePicker
                For I As Integer = 0 To objDateTimePicker.Length - 1 Step 1
                    objDateTimePicker(I).Enabled = False
                Next

            Case 1 '新增模式 
                'TextBox
                For I As Integer = 0 To objTextBox.Length - 1 Step 1
                    objTextBox(I).BackColor = Color.White
                    objTextBox(I).ReadOnly = False
                    objTextBox(I).Text = ""
                Next

                'ComboBox
                For I As Integer = 0 To objComboBox.Length - 1 Step 1
                    objComboBox(I).Enabled = True
                    objComboBox(I).Text = ""
                Next

                'RadioButton
                For I As Integer = 0 To objRadioButton.Length - 1 Step 1
                    objRadioButton(I).Enabled = True
                    objRadioButton(I).Checked = False
                Next

                'DateTimePicker
                For I As Integer = 0 To objDateTimePicker.Length - 1 Step 1
                    objDateTimePicker(I).Enabled = True
                    objDateTimePicker(I).Value = Now
                Next

            Case 2 '修改模式
                'TextBox
                For I As Integer = 0 To objTextBox.Length - 1 Step 1
                    objTextBox(I).BackColor = Color.White
                    objTextBox(I).ReadOnly = False
                Next

                'ComboBox
                For I As Integer = 0 To objComboBox.Length - 1 Step 1
                    objComboBox(I).Enabled = True
                Next

                'RadioButton
                For I As Integer = 0 To objRadioButton.Length - 1 Step 1
                    objRadioButton(I).Enabled = True
                Next

                'DateTimePicker
                For I As Integer = 0 To objDateTimePicker.Length - 1 Step 1
                    objDateTimePicker(I).Enabled = True
                Next

            Case 3 '複製模式
                'TextBox
                For I As Integer = 0 To objTextBox.Length - 1 Step 1
                    objTextBox(I).BackColor = Color.White
                    objTextBox(I).ReadOnly = False
                Next

                'ComboBox
                For I As Integer = 0 To objComboBox.Length - 1 Step 1
                    objComboBox(I).Enabled = True
                Next

                'RadioButton
                For I As Integer = 0 To objRadioButton.Length - 1 Step 1
                    objRadioButton(I).Enabled = True
                Next

                'DateTimePicker
                For I As Integer = 0 To objDateTimePicker.Length - 1 Step 1
                    objDateTimePicker(I).Enabled = True
                Next
        End Select
    End Sub

    'DataGridViewR 的控制項屬性
    Sub DataGridViewR_Control(ByVal objDataGridViewR() As DataGridView, ByVal Status As Integer)
        Select Case Status
            Case 0 '一般模式
                For I As Integer = 0 To objDataGridViewR.Length - 1 Step 1
                    objDataGridViewR(I).ReadOnly = True
                Next
            Case 1 '新增模式
                For I As Integer = 0 To objDataGridViewR.Length - 1 Step 1
                    objDataGridViewR(I).Rows.Clear()
                    objDataGridViewR(I).ReadOnly = False
                Next
            Case 2 '修改模式
                For I As Integer = 0 To objDataGridViewR.Length - 1 Step 1
                    objDataGridViewR(I).ReadOnly = False
                Next
            Case 3 '複製模式
                For I As Integer = 0 To objDataGridViewR.Length - 1 Step 1
                    objDataGridViewR(I).ReadOnly = False
                Next
        End Select
    End Sub

    '請取資料至Control
    Sub ReadData_Control(ByVal objTextBox() As TextBox, ByVal objComboBox() As ComboBox, ByVal objRadioButton() As RadioButton, ByVal objDateTimePicker() As DateTimePicker, ByVal objDR As SqlDataReader)
        'TextBox
        Dim objFieldName As String
        For I As Integer = 0 To objTextBox.Length - 1 Step 1
            objFieldName = objTextBox(I).Name
            objTextBox(I).Text = objDR(objFieldName).ToString
        Next
    End Sub
    '判斷欄位有無在　SqlDataReader　中
    Function ReaderExists(ByVal dr As SqlDataReader, ByVal columnName As String) As Boolean
        dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" & columnName & "'"
        Return (dr.GetSchemaTable().DefaultView.Count > 0)
    End Function
    '找尋某一容器下的所有控制項，傳入objDR99的值，要控制項的名稱 等於 欄位名稱才會塞值
    Sub ControlReadData(ByVal Ctl As Control, ByVal objDR As SqlDataReader)
        '判斷是否有子控制項
        If Ctl.Controls.Count > 0 Then
            For Each Ctl1 As Control In Ctl.Controls
                '繼續往下找(遞迴)
                ControlReadData(Ctl1, objDR)
            Next
        Else

            '若沒有就在這裡判斷控制項並結束遞迴
            If ReaderExists(objDR, Ctl.Name) Then
                If TypeOf Ctl Is TextBox Then
                    Ctl.Text = objDR(Ctl.Name).ToString
                End If

                If TypeOf Ctl Is ComboBox Then
                    Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                    cb.SelectedValue = objDR(Ctl.Name).ToString
                End If

                If TypeOf Ctl Is DateTimePicker Then
                    Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                    dp.Value = IIf(IsDate(objDR(Ctl.Name).ToString) = False, Now, objDR(Ctl.Name).ToString)
                End If

                If TypeOf Ctl Is CheckBox Then
                    Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                    If objDR(Ctl.Name).ToString = "True" Then
                        cb.Checked = True
                    Else
                        cb.Checked = False
                    End If
                End If
            End If
        End If
    End Sub
    '找尋某一容器下的所有控制項，依Control tag的值，決定是否可以編輯
    Sub Set_Control_RW(ByVal Ctl As Control, ByVal Status As Integer)
        '判斷是否有子控制項
        If Ctl.Controls.Count > 0 Then
            For Each Ctl1 As Control In Ctl.Controls
                '繼續往下找(遞迴)
                Set_Control_RW(Ctl1, Status)
            Next
        Else
            If Ctl.Tag = "MCtrl" Then
                '若沒有就在這裡判斷控制項並結束遞迴
                Select Case Status
                    Case 0 '一般模式
                        If TypeOf Ctl Is TextBox Then
                            Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                            tb.ReadOnly = True
                            tb.BackColor = Color.Ivory
                        End If
                        If TypeOf Ctl Is ComboBox Then
                            Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                            cb.Enabled = False
                        End If
                        If TypeOf Ctl Is RadioButton Then
                            Dim rb As System.Windows.Forms.RadioButton = DirectCast(Ctl, System.Windows.Forms.RadioButton)
                            rb.Enabled = False
                        End If
                        If TypeOf Ctl Is DateTimePicker Then
                            Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                            dp.Enabled = False
                        End If
                        If TypeOf Ctl Is CheckBox Then
                            Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                            cb.Enabled = False
                        End If
                        If TypeOf Ctl Is Button Then
                            Dim bt As System.Windows.Forms.Button = DirectCast(Ctl, System.Windows.Forms.Button)
                            bt.Enabled = False
                        End If
                        If TypeOf Ctl Is DataGridView Then
                            Dim dg As System.Windows.Forms.DataGridView = DirectCast(Ctl, System.Windows.Forms.DataGridView)
                            dg.Enabled = False
                        End If
                    Case 1 '新增模式 
                        If TypeOf Ctl Is TextBox Then
                            Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                            tb.BackColor = Color.White
                            tb.ReadOnly = False
                            tb.Text = ""
                        End If
                        If TypeOf Ctl Is ComboBox Then
                            Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                            cb.Enabled = True
                            cb.Text = ""
                        End If
                        If TypeOf Ctl Is RadioButton Then
                            Dim rb As System.Windows.Forms.RadioButton = DirectCast(Ctl, System.Windows.Forms.RadioButton)
                            rb.Enabled = True
                            rb.Checked = False
                        End If
                        If TypeOf Ctl Is DateTimePicker Then
                            Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                            dp.Enabled = True
                            dp.Value = Now
                        End If
                        If TypeOf Ctl Is CheckBox Then
                            Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                            cb.Enabled = True
                            cb.Checked = False
                        End If
                        If TypeOf Ctl Is Button Then
                            Dim bt As System.Windows.Forms.Button = DirectCast(Ctl, System.Windows.Forms.Button)
                            bt.Enabled = True
                        End If
                        If TypeOf Ctl Is DataGridView Then
                            Dim dg As System.Windows.Forms.DataGridView = DirectCast(Ctl, System.Windows.Forms.DataGridView)
                            dg.Enabled = True
                        End If
                    Case 2 '修改模式
                        If TypeOf Ctl Is TextBox Then
                            Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                            tb.BackColor = Color.Ivory
                            tb.ReadOnly = True
                            'tb.Enabled = True
                            'tb.Font = New Font("微軟正黑體", 12, FontStyle.Bold)
                        End If
                        If TypeOf Ctl Is ComboBox Then
                            Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                            cb.Enabled = False

                            cb.Font = New Font("微軟正黑體", 12, FontStyle.Bold)
                        End If
                        If TypeOf Ctl Is RadioButton Then
                            Dim rb As System.Windows.Forms.RadioButton = DirectCast(Ctl, System.Windows.Forms.RadioButton)
                            rb.Enabled = False
                        End If
                        If TypeOf Ctl Is DateTimePicker Then
                            Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                            dp.Enabled = False
                        End If
                        If TypeOf Ctl Is CheckBox Then
                            Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                            cb.Enabled = False
                        End If
                        If TypeOf Ctl Is Button Then
                            Dim bt As System.Windows.Forms.Button = DirectCast(Ctl, System.Windows.Forms.Button)
                            bt.Enabled = False
                        End If
                        If TypeOf Ctl Is DataGridView Then
                            Dim dg As System.Windows.Forms.DataGridView = DirectCast(Ctl, System.Windows.Forms.DataGridView)
                            dg.Enabled = False
                        End If
                    Case 3 '複製模式
                        If TypeOf Ctl Is TextBox Then
                            Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                            tb.BackColor = Color.White
                            tb.ReadOnly = False
                        End If
                        If TypeOf Ctl Is ComboBox Then
                            Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                            cb.Enabled = True
                        End If
                        If TypeOf Ctl Is RadioButton Then
                            Dim rb As System.Windows.Forms.RadioButton = DirectCast(Ctl, System.Windows.Forms.RadioButton)
                            rb.Enabled = True
                        End If
                        If TypeOf Ctl Is DateTimePicker Then
                            Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                            dp.Enabled = True
                        End If
                        If TypeOf Ctl Is CheckBox Then
                            Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                            cb.Enabled = True
                        End If
                        If TypeOf Ctl Is Button Then
                            Dim bt As System.Windows.Forms.Button = DirectCast(Ctl, System.Windows.Forms.Button)
                            bt.Enabled = True
                        End If
                        If TypeOf Ctl Is DataGridView Then
                            Dim dg As System.Windows.Forms.DataGridView = DirectCast(Ctl, System.Windows.Forms.DataGridView)
                            dg.Enabled = True
                        End If
                End Select
            End If

            If Ctl.Tag = "BCtrl" Then
                '若沒有就在這裡判斷控制項並結束遞迴
                Select Case Status
                    Case 0 '一般模式
                        If TypeOf Ctl Is TextBox Then
                            Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                            tb.BackColor = Color.Ivory
                            tb.ReadOnly = True
                            'tb.Font = New Font("微軟正黑體", 12, FontStyle.Bold)
                        End If
                        If TypeOf Ctl Is ComboBox Then
                            Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                            cb.Enabled = False
                        End If
                        If TypeOf Ctl Is RadioButton Then
                            Dim rb As System.Windows.Forms.RadioButton = DirectCast(Ctl, System.Windows.Forms.RadioButton)
                            rb.Enabled = False
                        End If
                        If TypeOf Ctl Is DateTimePicker Then
                            Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                            dp.Enabled = False
                        End If
                        If TypeOf Ctl Is CheckBox Then
                            Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                            cb.Enabled = False
                        End If
                        If TypeOf Ctl Is Button Then
                            Dim bt As System.Windows.Forms.Button = DirectCast(Ctl, System.Windows.Forms.Button)
                            bt.Enabled = False
                        End If
                        If TypeOf Ctl Is DataGridView Then
                            Dim dg As System.Windows.Forms.DataGridView = DirectCast(Ctl, System.Windows.Forms.DataGridView)
                            dg.Enabled = False
                        End If
                    Case 1 '新增模式 
                        If TypeOf Ctl Is TextBox Then
                            Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                            'tb.BackColor = Color.White
                            tb.ReadOnly = False
                            tb.Text = ""

                        End If
                        If TypeOf Ctl Is ComboBox Then
                            Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                            cb.Enabled = True
                            cb.Text = ""
                        End If
                        If TypeOf Ctl Is RadioButton Then
                            Dim rb As System.Windows.Forms.RadioButton = DirectCast(Ctl, System.Windows.Forms.RadioButton)
                            rb.Enabled = True
                            rb.Checked = False
                        End If
                        If TypeOf Ctl Is DateTimePicker Then
                            Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                            dp.Enabled = True
                            dp.Value = Now
                        End If
                        If TypeOf Ctl Is CheckBox Then
                            Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                            cb.Enabled = True
                            cb.Checked = False
                        End If
                        If TypeOf Ctl Is Button Then
                            Dim bt As System.Windows.Forms.Button = DirectCast(Ctl, System.Windows.Forms.Button)
                            bt.Enabled = True
                        End If
                        If TypeOf Ctl Is DataGridView Then
                            Dim dg As System.Windows.Forms.DataGridView = DirectCast(Ctl, System.Windows.Forms.DataGridView)
                            dg.Enabled = True
                        End If
                    Case 2 '修改模式
                        If TypeOf Ctl Is TextBox Then
                            Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                            'tb.BackColor = Color.White
                            tb.ReadOnly = False
                        End If
                        If TypeOf Ctl Is ComboBox Then
                            Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                            cb.Enabled = True
                        End If
                        If TypeOf Ctl Is RadioButton Then
                            Dim rb As System.Windows.Forms.RadioButton = DirectCast(Ctl, System.Windows.Forms.RadioButton)
                            rb.Enabled = True
                        End If
                        If TypeOf Ctl Is DateTimePicker Then
                            Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                            dp.Enabled = True
                        End If
                        If TypeOf Ctl Is CheckBox Then
                            Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                            cb.Enabled = True
                        End If
                        If TypeOf Ctl Is Button Then
                            Dim bt As System.Windows.Forms.Button = DirectCast(Ctl, System.Windows.Forms.Button)
                            bt.Enabled = True
                        End If
                        If TypeOf Ctl Is DataGridView Then
                            Dim dg As System.Windows.Forms.DataGridView = DirectCast(Ctl, System.Windows.Forms.DataGridView)
                            dg.Enabled = True
                        End If
                    Case 3 '複製模式
                        If TypeOf Ctl Is TextBox Then
                            Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                            tb.BackColor = Color.White
                            tb.ReadOnly = False
                        End If
                        If TypeOf Ctl Is ComboBox Then
                            Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                            cb.Enabled = True
                        End If
                        If TypeOf Ctl Is RadioButton Then
                            Dim rb As System.Windows.Forms.RadioButton = DirectCast(Ctl, System.Windows.Forms.RadioButton)
                            rb.Enabled = True
                        End If
                        If TypeOf Ctl Is DateTimePicker Then
                            Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                            dp.Enabled = True
                        End If
                        If TypeOf Ctl Is CheckBox Then
                            Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                            cb.Enabled = True
                        End If
                        If TypeOf Ctl Is Button Then
                            Dim bt As System.Windows.Forms.Button = DirectCast(Ctl, System.Windows.Forms.Button)
                            bt.Enabled = True
                        End If
                        If TypeOf Ctl Is DataGridView Then
                            Dim dg As System.Windows.Forms.DataGridView = DirectCast(Ctl, System.Windows.Forms.DataGridView)
                            dg.Enabled = True
                        End If
                End Select
            End If

        End If
    End Sub

    '找尋某一容器下的所有控制項，依Control tag的值，決定是否可以編輯
    Sub Set_Control_ZERO(ByVal Ctl As Control)
        '判斷是否有子控制項
        If Ctl.Controls.Count > 0 Then
            For Each Ctl1 As Control In Ctl.Controls
                '繼續往下找(遞迴)
                Set_Control_ZERO(Ctl1)
            Next
        Else
            If Ctl.Tag = "BCtrl" Then
                '若沒有就在這裡判斷控制項並結束遞迴
                If TypeOf Ctl Is TextBox Then
                    Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                    If tb.Text = "0" Then
                        tb.Text = ""
                    End If
                End If
            End If
        End If
    End Sub


    '找尋某一容器下的所有控制項，依Control tag的值，決定為空白
    Sub Set_Control_SPACE(ByVal Ctl As Control)
        '判斷是否有子控制項
        If Ctl.Controls.Count > 0 Then
            For Each Ctl1 As Control In Ctl.Controls
                '繼續往下找(遞迴)
                Set_Control_ZERO(Ctl1)
            Next
        Else
            If Ctl.Tag = "BCtrl" Then
                '若沒有就在這裡判斷控制項並結束遞迴
                If TypeOf Ctl Is TextBox Then
                    Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                    tb.Text = ""
                End If
            End If
        End If
    End Sub

    'GetCtrlUpdateSql 依 控制項 取得 Update SQL語法
    Sub GetCtrlUpdateSql(ByVal Ctl As Control, ByVal CtrlTag As String, ByRef UpdateSql As String)
        '判斷是否有子控制項
        If Ctl.Controls.Count > 0 Then
            For Each Ctl1 As Control In Ctl.Controls
                '繼續往下找(遞迴)
                GetCtrlUpdateSql(Ctl1, CtrlTag, UpdateSql)
            Next
        Else
            If Ctl.Tag = CtrlTag Then
                If TypeOf Ctl Is TextBox Then
                    Dim tb As System.Windows.Forms.TextBox = DirectCast(Ctl, System.Windows.Forms.TextBox)
                    UpdateSql &= tb.Name & " = '" & tb.Text & "',"
                End If
                If TypeOf Ctl Is ComboBox Then
                    Dim cb As System.Windows.Forms.ComboBox = DirectCast(Ctl, System.Windows.Forms.ComboBox)
                    UpdateSql &= cb.Name & " = '" & cb.SelectedValue & "',"
                End If
                If TypeOf Ctl Is DateTimePicker Then
                    Dim dp As System.Windows.Forms.DateTimePicker = DirectCast(Ctl, System.Windows.Forms.DateTimePicker)
                    If dp.Checked = True Then
                        UpdateSql &= dp.Name & " = '" & strSystemToDate(dp.Value.ToShortDateString) & "',"
                    Else
                        UpdateSql &= dp.Name & " = '',"
                    End If
                End If
                If TypeOf Ctl Is CheckBox Then
                    Dim cb As System.Windows.Forms.CheckBox = DirectCast(Ctl, System.Windows.Forms.CheckBox)
                    UpdateSql &= cb.Name & " = '" & cb.Checked.ToString & "',"

                End If
            End If
        End If
    End Sub
    '找尋某一容器下的ComboBox 使其進入後即可下拉
    Sub Set_Control_ComboBox(ByVal Ctl As Control)
        '判斷是否有子控制項
        If Ctl.Controls.Count > 0 Then
            For Each Ctl1 As Control In Ctl.Controls
                '繼續往下找(遞迴)
                Set_Control_ComboBox(Ctl1)
            Next
        Else
            If TypeOf Ctl Is ComboBox Then
                AddHandler Ctl.Enter, AddressOf ComboBox_Enter
            End If
        End If
    End Sub
    '找尋某一容器下的textbox,ComboBox 使其進入後 按ENTER UP DOWN 可以往上或往下
    Sub ControlFocus(ByVal Ctl As Control, keye As System.Windows.Forms.KeyEventArgs)

        '判斷是否有子控制項
        If Ctl.Controls.Count > 0 Then
            For Each Ctl1 As Control In Ctl.Controls
                '繼續往下找(遞迴)
                ControlFocus(Ctl1, keye)
            Next
        Else
            If Ctl.Focused Then
                If TypeOf Ctl Is TextBox Then
                    If keye.KeyCode = Keys.Enter Then SendKeys.Send("{tab}")
                    If keye.KeyCode = Keys.Down Then SendKeys.Send("{tab}")
                    If keye.KeyCode = Keys.Up Then SendKeys.Send("+{TAB}")
                End If
                If TypeOf Ctl Is ComboBox Then
                    If keye.KeyCode = Keys.Enter Then SendKeys.Send("{tab}")
                End If
            End If
        End If
    End Sub

    Sub ComboBox_Enter(sender As System.Object, e As System.EventArgs)
        Dim cb As System.Windows.Forms.ComboBox = DirectCast(sender, System.Windows.Forms.ComboBox)
        cb.DroppedDown = True
    End Sub

#End Region

#Region "物件動態產生驗證"
    'DataGridView
    Function DataGridView_DB(ByVal objDataGridView As DataGridView, ByVal strDNS As String, ByVal strSQL As String, ByVal arrRowName() As String, ByVal txtCount As ToolStripLabel) As Boolean
        Dim intSearchCount As Integer = 0 '查詢資料量
        Dim blnCheck As Boolean = False '是否有查詢到資料


        '產生薪資結構表供輸入
        objDataGridView.Rows.Clear() '清除DataGridView儲存格

        '開啟查詢
        objConSC = New SqlConnection(strDNS)
        objConSC.Open()

        strSQLSC = strSQL

        objCmdSC = New SqlCommand(strSQLSC, objConSC)
        objDRSC = objCmdSC.ExecuteReader

        Do While objDRSC.Read
            '--計算DataGridView的欄位數--
            I = objDataGridView.RowCount
            objDataGridView.RowCount = I + 1

            '--#顯示欄位值------
            For J As Integer = 0 To arrRowName.Length - 1 Step 1
                If ReaderExists(objDRSC, arrRowName(J)) Then
                    objDataGridView.CurrentCell = objDataGridView.Item(J, I)
                    objDataGridView.CurrentCell.Value = objDRSC(arrRowName(J)).ToString
                End If
            Next
            '--#End 顯示欄位值--

            intSearchCount += 1
            blnCheck = True
        Loop

        objDRSC.Close()    '關閉連結
        objConSC.Close()
        objCmdSC.Dispose() '手動釋放資源
        objConSC.Dispose()
        objConSC = Nothing '移除指標


        '異動後初始化*****
        txtCount.Text = intSearchCount
        Try : objDataGridView.CurrentCell = objDataGridView.Item(0, 0) : Catch ex As Exception : End Try '將值放回(0,0)儲存格
        Return blnCheck
    End Function
    Sub DataGridViewR_DB(ByVal objDataGridView As DataGridView, ByVal strDNS As String, ByVal strSQL As String, ByVal arrRowName() As String)
        '產生薪資結構表供輸入
        objDataGridView.Rows.Clear() '清除DataGridView儲存格

        '開啟查詢
        objConSC = New SqlConnection(strDNS)
        objConSC.Open()

        strSQLSC = strSQL

        objCmdSC = New SqlCommand(strSQLSC, objConSC)
        objDRSC = objCmdSC.ExecuteReader

        Do While objDRSC.Read
            '--計算DataGridView的欄位數--
            I = objDataGridView.RowCount
            objDataGridView.RowCount = I + 1

            '--#顯示欄位值------
            'For J As Integer = 0 To arrRowName.Length - 1 Step 1
            '    objDataGridView.CurrentCell = objDataGridView.Item(J, I)
            '    objDataGridView.CurrentCell.Value = objDRSC(arrRowName(J)).ToString
            'Next
            For J As Integer = 0 To arrRowName.Length - 1 Step 1
                If ReaderExists(objDRSC, arrRowName(J)) Then
                    objDataGridView.CurrentCell = objDataGridView.Item(J, I)
                    objDataGridView.CurrentCell.Value = objDRSC(arrRowName(J)).ToString
                End If
            Next
            '--#End 顯示欄位值--
        Loop

        objDRSC.Close()    '關閉連結
        objConSC.Close()
        objCmdSC.Dispose() '手動釋放資源
        objConSC.Dispose()
        objConSC = Nothing '移除指標


        '異動後初始化*****
        Try : objDataGridView.CurrentCell = objDataGridView.Item(0, 0) : Catch ex As Exception : End Try '將值放回(0,0)儲存格
    End Sub

    '依 DataGridView 的Header 去取值
    Function DataGridView_HeaderDB(ByVal objDataGridView As DataGridView, ByVal strDNS As String, ByVal strSQL As String) As Boolean
        Dim intSearchCount As Integer = 0 '查詢資料量
        Dim blnCheck As Boolean = False '是否有查詢到資料


        '產生薪資結構表供輸入

        objDataGridView.Rows.Clear() '清除DataGridView儲存格



        '開啟查詢
        objConSC = New SqlConnection(strDNS)
        objConSC.Open()
        strSQLSC = strSQL

        objCmdSC = New SqlCommand(strSQLSC, objConSC)
        objCmdSC.CommandTimeout = 120
        objDRSC = objCmdSC.ExecuteReader

        Do While objDRSC.Read
            '--計算DataGridView的欄位數--
            I = objDataGridView.RowCount
            objDataGridView.RowCount = I + 1

            '--#顯示欄位值------
            For J As Integer = 0 To objDataGridView.ColumnCount - 1 Step 1
                If ReaderExists(objDRSC, objDataGridView.Columns(J).HeaderText) Then
                    objDataGridView.CurrentCell = objDataGridView.Item(J, I)
                    objDataGridView.CurrentCell.Value = objDRSC(objDataGridView.Columns(J).HeaderText).ToString
                End If
            Next
            '--#End 顯示欄位值--

            intSearchCount += 1
            blnCheck = True
        Loop

        objDRSC.Close()    '關閉連結
        objConSC.Close()
        objCmdSC.Dispose() '手動釋放資源
        objConSC.Dispose()
        objConSC = Nothing '移除指標


        '異動後初始化*****
        'txtCount.Text = intSearchCount
        Try : objDataGridView.CurrentCell = objDataGridView.Item(0, 0) : Catch ex As Exception : End Try '將值放回(0,0)儲存格
        Return blnCheck
    End Function
    'ComboBox
    Sub ComboBox_DB(ByVal objComboBox As ComboBox, ByVal strDNS As String, ByVal strTableName As String, ByVal strRowID As String, ByVal strRowName As String, Optional ByVal strWhere As String = "", Optional ByVal strOrderBy As String = "")
        Dim List As New DataTable()
        List.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
        List.Columns.Add(New DataColumn("NAME", System.Type.GetType("System.String")))


        '加入顯示
        '開啟產生
        objConSC = New SqlConnection(strDNS)
        objConSC.Open()

        strSQLSC = "SELECT " & strRowID & " AS strID, " & strRowName & " AS strName FROM " & strTableName
        If strWhere <> "" Then strSQLSC &= " WHERE " & strWhere
        If strOrderBy <> "" Then strSQLSC &= " ORDER BY " & strOrderBy

        objCmdSC = New SqlCommand(strSQLSC, objConSC)
        objDRSC = objCmdSC.ExecuteReader

        I = 1
        List.Rows.Add(List.NewRow())
        List.Rows(0)(0) = "" : List.Rows(0)(1) = ""
        Do While objDRSC.Read
            List.Rows.Add(List.NewRow())
            List.Rows(I)(0) = Trim(objDRSC("strID").ToString)
            List.Rows(I)(1) = Trim(objDRSC("strName").ToString)

            I += 1
        Loop

        objDRSC.Close()    '關閉連結
        objConSC.Close()
        objCmdSC.Dispose() '手動釋放資源
        objConSC.Dispose()
        objConSC = Nothing '移除指標


        objComboBox.DataSource = List
        objComboBox.ValueMember = "ID"
        objComboBox.DisplayMember = "NAME"
    End Sub
    Sub ComboBox_DGDB(ByVal objComboBox As DataGridViewComboBoxColumn, ByVal strDNS As String, ByVal strTableName As String, ByVal strRowID As String, ByVal strRowName As String, Optional ByVal strWhere As String = "", Optional ByVal strOrderBy As String = "")
        Dim List As New DataTable()
        List.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
        List.Columns.Add(New DataColumn("NAME", System.Type.GetType("System.String")))


        '加入顯示
        '開啟產生
        objConSC = New SqlConnection(strDNS)
        objConSC.Open()

        strSQLSC = "SELECT " & strRowID & " AS strID, " & strRowName & " AS strName FROM " & strTableName
        If strWhere <> "" Then strSQLSC &= " WHERE " & strWhere
        If strOrderBy <> "" Then strSQLSC &= " ORDER BY " & strOrderBy

        objCmdSC = New SqlCommand(strSQLSC, objConSC)
        objDRSC = objCmdSC.ExecuteReader

        I = 1
        List.Rows.Add(List.NewRow())
        List.Rows(0)(0) = "" : List.Rows(0)(1) = ""
        Do While objDRSC.Read
            List.Rows.Add(List.NewRow())
            List.Rows(I)(0) = Trim(objDRSC("strID").ToString)
            List.Rows(I)(1) = Trim(objDRSC("strName").ToString)

            I += 1
        Loop

        objDRSC.Close()    '關閉連結
        objConSC.Close()
        objCmdSC.Dispose() '手動釋放資源
        objConSC.Dispose()
        objConSC = Nothing '移除指標


        objComboBox.DataSource = List
        objComboBox.ValueMember = "ID"
        objComboBox.DisplayMember = "NAME"
    End Sub
    Sub ComboBox_GG(ByVal objComboBox As ComboBox, ByVal strRowID As String, ByVal strRowName As String)
        Dim List As New DataTable()
        List.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
        List.Columns.Add(New DataColumn("NAME", System.Type.GetType("System.String")))

        Dim strID() As String = strRowID.Split(",")
        Dim strName() As String = strRowName.Split(",")

        '加入顯示
        For I As Integer = 0 To strID.Length - 1 Step 1
            List.Rows.Add(List.NewRow())
            List.Rows(I)(0) = strID(I)
            List.Rows(I)(1) = strName(I)
        Next

        objComboBox.DataSource = List
        objComboBox.ValueMember = "ID"
        objComboBox.DisplayMember = "NAME"
    End Sub

    '取得螢幕解析度(寬、高)
    Function Bounds_Width() As Integer
        Dim intValue As Integer = 0
        Dim strNowScreenWidth As Integer = Screen.PrimaryScreen.Bounds.Width '目前解析度寬度

        intValue = strNowScreenWidth - 25 '需減除其他部份

        Return intValue
    End Function
    '取得工作區域(寬、高)
    Function WorkingArea_Width() As Integer
        Dim intValue As Integer = 0
        Dim strNowScreenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width '目前解析度寬度

        intValue = strNowScreenWidth - 45

        Return intValue
    End Function

    'sql 匯出EXCEL 
    Sub SqlToExcel(ByVal strDNS As String, ByVal strSQL As String)

        ''開啟查詢
        'objConSC = New SqlConnection(strDNS)
        'objConSC.Open()

        'strSQLSC = strSQL

        'objCmdSC = New SqlCommand(strSQLSC, objConSC)
        'objDRSC = objCmdSC.ExecuteReader

        'Dim wb As IWorkbook = New HSSFWorkbook
        'Dim ws As ISheet


        'Dim dt As DataTable = New DataTable()
        'dt.Load(objDRSC, LoadOption.OverwriteChanges)

        'If dt.TableName <> String.Empty Then
        '    ws = wb.CreateSheet(dt.TableName)
        'Else
        '    ws = wb.CreateSheet("Sheet1")
        'End If

        'ws.CreateRow(0) '第一行為欄位名稱
        'Dim i As Integer
        'For i = 0 To dt.Columns.Count - 1 Step i + 1
        '    ws.GetRow(0).CreateCell(i).SetCellValue(dt.Columns(i).ColumnName)
        'Next

        'For i = 0 To dt.Rows.Count - 1
        '    ws.CreateRow(i + 1)
        '    Dim j As Integer = 0
        '    For j = 0 To dt.Columns.Count - 1
        '        ws.GetRow(i + 1).CreateCell(j).SetCellValue(dt.Rows(i)(j).ToString())
        '    Next
        'Next

        'Dim file As FileStream = New FileStream("npoi.xls", FileMode.Create) '產生檔案
        'wb.Write(file)
        'file.Close()

        'Dim process As New Process
        'process.Start(Application.StartupPath + "\npoi.xls")

        'objDRSC.Close()    '關閉連結
        'objConSC.Close()
        'objCmdSC.Dispose() '手動釋放資源
        'objConSC.Dispose()
        'objConSC = Nothing '移除指標

    End Sub
#End Region
#End Region

#Region "系統加解密"
    '3DES加密
    Function Encrypt3DES(ByVal a_strString As String) As String
        Dim a_strKey = "0123456789abcdefhijklmno"
        Dim DES As TripleDESCryptoServiceProvider
        Dim DESEncrypt As ICryptoTransform
        Dim Buffer() As Byte

        DES = New TripleDESCryptoServiceProvider
        DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey)
        DES.Mode = CipherMode.ECB
        DESEncrypt = DES.CreateEncryptor()
        Buffer = ASCIIEncoding.UTF8.GetBytes(a_strString)

        Return GetRndString() & Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length)) & GetRndString()
    End Function
    '3DES解密
    Function Decrypt3DES(ByVal strValue As String) As String
        Dim a_strString As String = Mid(strValue, 5, Len(strValue) - 8)
        Dim a_strKey = "0123456789abcdefhijklmno"

        Dim DES As TripleDESCryptoServiceProvider
        Dim DESDecrypt As ICryptoTransform
        Dim result As String
        Dim buffer() As Byte

        DES = New TripleDESCryptoServiceProvider()
        DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey)
        DES.Mode = CipherMode.ECB
        DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7
        DESDecrypt = DES.CreateDecryptor()
        result = ""

        Try
            buffer = Convert.FromBase64String(a_strString)
            result = ASCIIEncoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length))
        Catch e As Exception

        End Try

        Return result
    End Function

    'MD5加密
    Function EncryptMD5(ByVal pToEncrypt As String) As String
        Dim sKey As String = "8lvbe4kE"

        Dim des As New DESCryptoServiceProvider()
        Dim inputByteArray As Byte() = Encoding.Default.GetBytes(pToEncrypt)
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey)
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey)

        Dim ms As New MemoryStream()
        Dim cs As New CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write)
        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()

        Dim ret As New StringBuilder()

        For Each b As Byte In ms.ToArray()
            ret.AppendFormat("{0:X2}", b)
        Next
        ret.ToString()

        Return GetRndString() & ret.ToString() & GetRndString()
    End Function
    'MD5解密
    Function DecryptMD5(ByVal strValue As String) As String
        Dim strString As String = ""

        Try
            Dim pToDecrypt As String = Mid(strValue, 5, Len(strValue) - 8)
            Dim sKey As String = "8lvbe4kE"

            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray As Byte() = New Byte(pToDecrypt.Length / 2 - 1) {}
            For x As Integer = 0 To pToDecrypt.Length / 2 - 1
                Dim i As Integer = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16))
                inputByteArray(x) = CByte(i)
            Next
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey)
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey)

            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()

            Dim ret As New StringBuilder()
            strString = System.Text.Encoding.Default.GetString(ms.ToArray())
        Catch ex As Exception

        End Try

        Return strString
    End Function

    '隨機產生編碼(4碼)
    Function GetRndString()
        GetRndString = ""

        Dim i As Integer
        Dim intLength As Short
        Const STRINGSOURCE As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        intLength = Len(STRINGSOURCE) - 1

        Randomize()

        For i = 1 To 4
            GetRndString = GetRndString & Mid(STRINGSOURCE, Int(Rnd() * intLength + 1), 1)
        Next

        Return GetRndString
    End Function
#End Region

    Function GetYear(ByVal sDate As Date) As Integer
        Dim sYear As String = CStr(Year(sDate))
        Dim Year3 As Integer

        If sYear.Length = 4 Then
            Year3 = Year(sDate) - 1911
        Else
            Year3 = Year(sDate)
        End If

        Return Year3
    End Function
End Module