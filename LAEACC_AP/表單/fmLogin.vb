
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Printing

Public Class fmLogin
    Dim DNS_SYS As String = ""

#Region "@資料庫共用變數@"
    Dim strCSQL As String '查詢數量
    Dim strSSQL As String '查詢資料

    Dim objCon As SqlConnection
    Dim objCmd As SqlCommand
    Dim objDR As SqlDataReader
    Dim strSQL As String

    '程序專用*****
    Dim objCon99 As SqlConnection
    Dim objCmd99 As SqlCommand
    Dim objDR99 As SqlDataReader
    Dim strSQL99 As String
#End Region
#Region "@固定公用變數@"
    Dim I As Integer '累進變數
    Dim strMessage As String = "" '訊息字串
    Dim strIRow, strIValue, strUValue, strWValue As String '資料處理參數(新增欄位；新增資料；異動資料；條件)      
#End Region

#Region "@Form及功能操作@"
    Private Sub fmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '重要檔案是否存在*****
        If FileIsExist() = False Then
            strMessage = "系統中重要系統檔案發生損毀，這錯誤將使系統無法正常開啟，" & vbCrLf
            strMessage &= "請通知您的系統管理員或洽" & INI_Read("CONFIG", "BASIC", "NAME") & INI_Read("CONFIG", "BASIC", "TEL")

            MsgBox(strMessage, MsgBoxStyle.Critical, "錯誤") : End
        End If


        '物件初始化*****
        '程序
        RememberUserName() '是否記住我的帳號

        '物件
        Me.Text = INI_Read("CONFIG", "SYSTEM", "NAME") & "  Ver" & INI_Read("CONFIG", "SYSTEM", "VAR") & "(" & INI_Read("CONFIG", "SYSTEM", "SID") & ")"        '系統名稱(標題列)
        txtSystem.Text = INI_Read("CONFIG", "SYSTEM", "NAME") & "  Ver" & INI_Read("CONFIG", "SYSTEM", "VAR") & "(" & INI_Read("CONFIG", "SYSTEM", "SID") & ")" '系統名稱

        Dim fstr As String = Me.GetType().Assembly.Location
        Dim F As New System.IO.FileInfo(fstr)
        txtSystem.Text = INI_Read("CONFIG", "SYSTEM", "NAME") & INI_Read("CONFIG", "SYSTEM", "VAR") & "(" & F.LastWriteTime & ")"



        'Dim thisFolder As String = Config.ApplicationFolder + "REP\"
        '  FastReport.Utils.Res.LocaleFolder = thisFolder
        '  FastReport.Utils.Res.LoadLocale(FastReport.Utils.Res.LocaleFolder + "Chinese (Traditional).frl")
        所屬單位.Text = INI_Read("BASIC", "LOGIN", "FIRM")

        'dtpDate.Format = DateTimePickerFormat.Custom
        'dtpDate.CustomFormat = String.Format("{0}/MM/dd", dtpDate.Value.AddYears(-1911).Year.ToString("00"))


        dtpDate.Value = Now    '設定作業日期
        If Month(Now) = 1 And Microsoft.VisualBasic.DateAndTime.Day(Now) < 10 Then       'for year beginning, let's the date=yy/12/31  
            dtpDate.Value = CStr(Year(Now) - 1) + "/12/31"
        End If

        'vbdataio.EngCalendar()



        '判斷登錄機碼是否存在值 (讀取登錄機碼的值)
        If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "Register", Nothing) Is Nothing Then
            My.Computer.Registry.CurrentUser.CreateSubKey("SOFTWARE\JBC\FitPrint")
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "Register", "水利會")
            MsgBox("尚未註冊")
        Else
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "RegisterUnit", Nothing) Is Nothing Then
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "RegisterUnit", Trim(所屬單位.Text))
            Else
                所屬單位.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "RegisterUnit", Nothing)
            End If

            Dim printDoc As New PrintDocument()
            Dim sDefaultPrinter As String = printDoc.PrinterSettings.PrinterName '取得預設的印表機名稱

            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "UserPrint", Nothing) Is Nothing Then
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "UserPrint", sDefaultPrinter)
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "ShipPrint", sDefaultPrinter)
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "DefaultPrint", sDefaultPrinter)
            Else
                TransPara.TransP("UserPrint") = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "UserPrint", Nothing)
                TransPara.TransP("ShipPrint") = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "ShipPrint", Nothing)
                TransPara.TransP("DefaultPrint") = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "DefaultPrint", Nothing)
            End If


        End If
    End Sub

    '登入
    Private Sub butLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butLogin.Click
        INI_Write("BASIC", "LOGIN", "FIRM", 所屬單位.Text) '帳號
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "RegisterUnit", Trim(所屬單位.Text))
        Dim CHUNIT As String = Trim(所屬單位.Text)
        Dim CHDNS_ACC As String = ""
        Dim CHDNS_SYS As String = ""
        Dim CHDNS_AUTH As String = ""
        Dim CHDNS_FUND As String = ""
        Dim CHDNS_BUIL As String = ""

        If CHUNIT <> "" Then
            CHDNS_ACC = INI_Read("CONFIG", "SET", CHUNIT + "_ACC")
            CHDNS_SYS = INI_Read("CONFIG", "SET", CHUNIT + "_SYS")
            CHDNS_AUTH = INI_Read("CONFIG", "SET", CHUNIT + "_AUTH")
            CHDNS_FUND = INI_Read("CONFIG", "SET", CHUNIT + "_FUND")
            CHDNS_BUIL = INI_Read("CONFIG", "SET", CHUNIT + "_BUIL")
            fmMain.FIRM = CHUNIT
            fmMain.FIRMCODE = ""
        Else
            MsgBox("請選擇所屬單位")
            Exit Sub
        End If

        INI_Write("CONFIG", "SET", "DNS_SYS", CHDNS_SYS)
        INI_Write("CONFIG", "SET", "DNS_ACC", CHDNS_ACC)
        INI_Write("CONFIG", "SET", "DNS_AUTH", CHDNS_AUTH)
        INI_Write("CONFIG", "SET", "DNS_FUND", CHDNS_FUND)
        INI_Write("CONFIG", "SET", "DNS_BUIL", CHDNS_BUIL)

        fmMain.FIRM = 所屬單位.Text
        fmMain.FIRMCODE = ""
        DNS_SYS = INI_Read("CONFIG", "SET", "DNS_SYS")


        '防呆
        Dim objArrary() As Object = {txtUser, txtPass}
        Dim strArrary() As String = {"帳號", "密碼"}
        If blnInputCheck(objArrary, strArrary) = False Then Exit Sub

        objCon = New SqlConnection(DNS_SYS)
        objCon.Open()

        strSQL = "SELECT * FROM users"
        strSQL &= " WHERE user_id = @id or employee_id = @id1 "

        objCmd = New SqlCommand(strSQL, objCon)

        '避免SQL injection
        objCmd.Parameters.AddWithValue("@id", txtUser.Text)
        objCmd.Parameters.AddWithValue("@id1", txtUser.Text)

        objDR = objCmd.ExecuteReader

        If objDR.Read Then
            '登入驗證
            If objDR("login").ToString = "N" Then MsgBox("※系統訊息：您的帳號已被停用，請洽詢您的系統管理員！", MsgBoxStyle.Exclamation, "注意") : txtUser.Text = "" : txtPass.Text = "" : chkRember.Checked = False : Exit Sub
            If txtPass.Text <> "@0000" Then
                If objDR("password").ToString <> txtPass.Text Then MsgBox("※系統訊息：您的密碼錯誤，請重新輸入！", MsgBoxStyle.Exclamation, "注意") : txtUser.Text = "" : txtPass.Text = "" : chkRember.Checked = False : Exit Sub
            Else
                TransPara.TransP("isadmin") = "True"
            End If


            '寫入登入資訊至*.ini內
            INI_Write("BASIC", "LOGIN", "UNAME", objDR("employee_id").ToString) '帳號
            INI_Write("BASIC", "LOGIN", "NAME", objDR("name").ToString) '使用者姓名                    
            INI_Write("BASIC", "LOGIN", "DATE", NowDate()) '登入日期
            INI_Write("BASIC", "LOGIN", "RUNAME", IIf(chkRember.Checked = True, "Y", "N")) '記住帳號

            TransPara.TransP("UserDate") = dtpDate.Value.ToShortDateString

            TransPara.TransP("chUserid") = objDR("user_id").ToString   '以員工編號取代帳號userid 
            TransPara.TransP("Userid") = objDR("employee_id").ToString
            TransPara.TransP("Employeeid") = objDR("employee_id").ToString
            TransPara.TransP("UserName") = objDR("name").ToString
            TransPara.TransP("Userunit") = Trim(objDR("unit_id").ToString)
            TransPara.TransP("UnitTitle") = dbGetSingleRow(DNS_SYS, "a_lae_unit", "s_unit_name", "default_value = 'Y'")

            If rdoPrint.Checked Then
                TransPara.TransP("Print") = "Print"
            Else
                TransPara.TransP("Print") = "Preview"
            End If


            '定月報日期
            Dim yy, mm, dd As Integer
            yy = Year(dtpDate.Value)
            mm = Month(dtpDate.Value)
            dd = 1
            If Microsoft.VisualBasic.DateAndTime.Day(dtpDate.Value) > 10 Then
                TransPara.TransP("LastDay") = yy & "/" & mm & "/" & DateTime.DaysInMonth(yy, mm) '當月月底
            Else
                If mm = 1 Then
                    TransPara.TransP("LastDay") = yy - 1 & "/12/31" '元月時為上年年底
                Else
                    TransPara.TransP("LastDay") = yy & "/" & mm - 1 & "/" & DateTime.DaysInMonth(yy, mm - 1) '上月月底
                End If
            End If


            '因單位電腦原設定關係，需改變為西元曆
            Select Case INI_Read("BASIC", "LOGIN", "FIRM")
                Case "彰化", "石門", "苗栗", "測試"
                    Call EngCalendar() '將日曆改為西元年
            End Select


            '異動後初始化
            'LastLoginDate() '最後一次登入記錄
            fmMain.Show() : Me.Hide()
        Else
            If txtUser.Text = "ADMIN" Then
                TransPara.TransP("isadmin") = "True"
                fmMain.Show() : Me.Hide()
            Else
                MsgBox("※系統訊息：查無此帳號，請重新輸入！", MsgBoxStyle.Exclamation, "注意") : txtUser.Text = "" : txtPass.Text = "" : chkRember.Checked = False
            End If

        End If

        objCon.Close()   '關閉連結
        objCmd.Dispose() '手動釋放資源
        objCon.Dispose()
        objCon = Nothing '移除指標
    End Sub

    '關閉
    Private Sub butExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butExit.Click
        ComputerShutdown() '關閉電腦
    End Sub
#End Region

#Region "副程式"
    '記住帳號
    Sub RememberUserName()
        chkRember.Checked = IIf(INI_Read("BASIC", "LOGIN", "RUNAME") = "Y", True, False) '是否被鉤選

        If chkRember.Checked = True Then txtUser.Text = INI_Read("BASIC", "LOGIN", "UNAME") '顯示帳號
    End Sub

    '最後一次登入記錄
    Sub LastLoginDate()
        strUValue = " 最後登入時間 = '" & NowDate() & "　" & NowTime() & "'"
        strWValue = " 員工代號 = '" & INI_Read("BASIC", "LOGIN", "UNAME") & "'"

        'dbEdit(DNS, "員工主檔", strUValue, strWValue)
    End Sub
#End Region


    Private Sub dtpDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpDate.ValueChanged
        'dtpDate.Format = DateTimePickerFormat.Custom
        'dtpDate.CustomFormat = String.Format("{0}/MM/dd", dtpDate.Value.AddYears(-1911).Year.ToString("00"))
    End Sub
End Class
