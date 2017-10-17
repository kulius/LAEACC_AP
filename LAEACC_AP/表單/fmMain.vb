Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Printing

Public Class fmMain
    Dim DNS As String = INI_Read("CONFIG", "SET", "DNS_SYS") 'DNS設定值
    Public FIRM As String
    Public FIRMCODE As String
    Dim sqlstr, strSQL, strSQL1 As String
    Dim bmS, bmT As BindingManagerBase
    Dim myDatasetS As DataSet
    Dim DNS_SYS As String = INI_Read("CONFIG", "SET", "DNS_SYS")
    Public DNS_ACC As String
    Dim 所屬單位 As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "RegisterUnit", Nothing)


#Region "@資料庫共用變數@"
    Dim strCSQL As String '查詢數量
    Dim strSSQL As String '查詢資料

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
    Private Sub fmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '++ 物件初始化 ++
        'UsersPower(INI_Read("BASIC", "LOGIN", "UNAME")) '已授權系統權限
        'H01N0310.Text = "關於 " & INI_Read("CONFIG", "SYSTEM", "NAME") & "(&A)"

        DNS = INI_Read("CONFIG", "SET", "DNS_SYS") 'DNS設定值
        DNS_SYS = INI_Read("CONFIG", "SET", "DNS_SYS")

        '系統狀態
        Me.Text = FIRM & "✯✯✯" & INI_Read("CONFIG", "SYSTEM", "NAME") & "✯✯✯" '系統名稱        
        'txtPhone.Text = "維修專線：" & INI_Read("CONFIG", "BASIC", "TEL") '維修專線
        txtPhone.Text = "單位：" & FIRM
        txtUser.Text = "系統登入者：" & INI_Read("BASIC", "LOGIN", "NAME") '登入者
        txtDate.Text = NowDate() & "　" & NowTime() '小時鐘

        Me.Controls(Me.Controls.Count - 1).BackColor = Me.BackColor

        'Dim fmMenu As New fmMenu

        'fmMenu.MdiParent = Me
        'fmMenu.Show()

        strSQL = "select DISTINCT c.s_system_id, d.s_system_name, d.sort "
        strSQL &= " from groups_power a "
        strSQL &= " inner join users_groups b on a.group_id=b.group_id and b.user_id = '" & TransPara.TransP("chUserid") & "'"
        strSQL &= " INNER JOIN a_sys_nunit_item c on a.s_unitem_id=c.s_unitem_id"
        strSQL &= " INNER JOIN a_sys_name d ON d.s_system_id=c.s_system_id"
        strSQL &= " union "
        strSQL &= " select DISTINCT c.s_system_id, d.s_system_name, d.sort "
        strSQL &= " from groups_power a "
        strSQL &= " inner join unit_groups b on a.group_id=b.group_id and CHARINDEX(b.unit_id, '" & TransPara.TransP("Userunit") & "')>0 "
        strSQL &= " INNER JOIN a_sys_nunit_item c on a.s_unitem_id=c.s_unitem_id"
        strSQL &= " INNER JOIN a_sys_name d ON d.s_system_id=c.s_system_id"

        If TransPara.TransP("isadmin") = "True" Then
            strSQL = "   select DISTINCT c.s_system_id, d.s_system_name, d.sort "
            strSQL &= " from a_sys_nunit_item c "
            strSQL &= " INNER JOIN a_sys_name d ON d.s_system_id=c.s_system_id"
            strSQL &= " order by d.sort"
        End If

        myDatasetS = openmember(DNS_SYS, "a_sys_name", strSQL)
        bmS = Me.BindingContext(myDatasetS, "a_sys_name")

        Dim x_pos As Integer = 250
        Dim y_pos As Integer = 70
        Dim x As Integer = 0
        Dim y As Integer = 1
        Dim FirstBt As Button

        For I = 0 To bmS.Count - 1
            bmS.Position = I
            x = x + 1
            If I Mod 3 = 0 Then
                y = y + 1
                x = 1
            End If



            Dim bt As Button = New Button
            bt.Name = bmS.Current("s_system_id")
            If FirstBt Is Nothing Then
                FirstBt = bt
            End If

            bt.Text = bmS.Current("s_system_name")
            bt.Location = New System.Drawing.Point((x_pos * x), (y_pos * y))
            bt.Size = New System.Drawing.Size(220, 50)
            Me.Panel1.Controls.Add(bt)
            AddHandler bt.Click, AddressOf Me.bt_Click
        Next

        If TransPara.TransP("isadmin") = "True" Then
            Dim bt1 As Button = New Button
            bt1.Name = "RunSQL"
            bt1.Text = "執行SQL語法"
            bt1.Location = New System.Drawing.Point((x_pos * 1), (y_pos * 6))
            bt1.Size = New System.Drawing.Size(220, 50)
            Me.Panel1.Controls.Add(bt1)
            AddHandler bt1.Click, AddressOf Me.RunSQL_Click
        End If



        '取得安裝於電腦上的所有印表機名稱，加入 ListBox (Name : lbInstalledPrinters) 中
        For Each strPrinter As String In PrinterSettings.InstalledPrinters
            Me.CBShipPrint.Items.Add(strPrinter)
            Me.CBDefaultPrint.Items.Add(strPrinter)
        Next
        Me.CBShipPrint.SelectedIndex = Me.CBShipPrint.FindString(TransPara.TransP("ShipPrint"))
        Me.CBDefaultPrint.SelectedIndex = Me.CBDefaultPrint.FindString(TransPara.TransP("DefaultPrint"))



        FirstBt.PerformClick()
    End Sub
    Private Sub fmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '因單位電腦舊系統設定關係，需改變為民國年
        Select Case INI_Read("BASIC", "LOGIN", "FIRM")
            Case "彰化", "桃園", "石門", "苗栗", "測試"
                Call ChtCalendar() '將日曆改為民國年
        End Select

        ComputerShutdown() '關閉電腦
    End Sub

    Private Sub bt_Click(sender As Object, e As EventArgs)
        Dim CHUNIT As String = 所屬單位
        Dim CHDNS_ACC As String = ""
        Dim CHDNS_FUND As String = ""
        Dim CHDNS_BUIL As String = ""
        Dim CHDNS_DB As String = ""
        If InStr(1, sender.text, "退撫") Then
            CHDNS_FUND = INI_Read("CONFIG", "SET", CHUNIT + "_FUND")
            INI_Write("CONFIG", "SET", "DNS_ACC", CHDNS_FUND)
            CHDNS_DB = "FUNDDB"
        ElseIf InStr(1, sender.text, "輔建") Then
            CHDNS_BUIL = INI_Read("CONFIG", "SET", CHUNIT + "_BUIL")
            INI_Write("CONFIG", "SET", "DNS_ACC", CHDNS_BUIL)
            CHDNS_DB = "BUILDB"
        Else
            CHDNS_ACC = INI_Read("CONFIG", "SET", CHUNIT + "_ACC")
            INI_Write("CONFIG", "SET", "DNS_ACC", CHDNS_ACC)
            CHDNS_DB = "ACCDB"
        End If

        DNS_ACC = INI_Read("CONFIG", "SET", "DNS_ACC")
        txtDate.Text = "目前連結資料庫：" + CHDNS_DB

        Me.MenuStrip.Items.Clear()

        'sqlstr = " select * from a_sys_nunit where s_system_id =  '" + sender.name + "'"

        strSQL = "SELECT DISTINCT m.s_unit_id, t1.s_unit_name, t1.s_unit_css FROM a_sys_nunit_item m"
        strSQL &= " INNER JOIN a_sys_nunit t1 ON m.s_system_id = t1.s_system_id AND m.s_unit_id = t1.s_unit_id "
        strSQL &= " INNER JOIN groups_power t2 ON m.s_unitem_id = t2.s_unitem_id"
        strSQL &= " inner join users_groups t3 on t2.group_id=t3.group_id and t3.user_id = '" & TransPara.TransP("chUserid") & "'"
        strSQL &= " WHERE m.s_system_id = '" & sender.name & "'"
        strSQL &= " union "
        strSQL &= " SELECT DISTINCT m.s_unit_id, t1.s_unit_name, t1.s_unit_css FROM a_sys_nunit_item m"
        strSQL &= "  INNER JOIN a_sys_nunit t1 ON m.s_system_id = t1.s_system_id AND m.s_unit_id = t1.s_unit_id "
        strSQL &= "  INNER JOIN groups_power t2 ON m.s_unitem_id = t2.s_unitem_id"
        strSQL &= "  inner join unit_groups t3 on t2.group_id=t3.group_id and CHARINDEX(t3.unit_id, '" & TransPara.TransP("Userunit") & "')>0 "
        strSQL &= "  WHERE m.s_system_id = '" & sender.name & "'"
        strSQL &= " ORDER BY m.s_unit_id"

        If TransPara.TransP("isadmin") = "True" Then
            strSQL = "SELECT DISTINCT m.s_unit_id, t1.s_unit_name, t1.s_unit_css FROM a_sys_nunit_item m"
            strSQL &= " INNER JOIN a_sys_nunit t1 ON m.s_system_id = t1.s_system_id AND m.s_unit_id = t1.s_unit_id "
            strSQL &= " WHERE m.s_system_id = '" & sender.name & "'"
        End If


        myDatasetS = openmember(DNS_SYS, "a_sys_nunit", strSQL)
        bmS = Me.BindingContext(myDatasetS, "a_sys_nunit")

        For I = 0 To bmS.Count - 1
            bmS.Position = I

            Dim rootmenu As ToolStripMenuItem = New ToolStripMenuItem
            rootmenu.Name = bmS.Current("s_unit_id")
            rootmenu.Text = bmS.Current("s_unit_name")

            'sqlstr = " select * from a_sys_nunit_item where s_system_id =  '" + sender.name + "' and s_unit_id= '" + bmS.Current("s_unit_id") + "' order by sort"

            strSQL1 = "SELECT DISTINCT m.s_unitem_id, m.s_item_name,m.sort,m.s_system_id,m.s_unit_id,m.custom_path,m.custom_value FROM a_sys_nunit_item m"
            strSQL1 &= " INNER JOIN groups_power t1 ON m.s_unitem_id = t1.s_unitem_id"
            strSQL1 &= " inner join users_groups t2 on t1.group_id=t2.group_id and t2.user_id = '" & TransPara.TransP("chUserid") & "'"
            strSQL1 &= " WHERE m.s_system_id = '" & sender.name & "'"
            strSQL1 &= " AND m.s_unit_id = '" & bmS.Current("s_unit_id") & "'"
            strSQL1 &= " union "
            strSQL1 &= " SELECT DISTINCT m.s_unitem_id, m.s_item_name,m.sort,m.s_system_id,m.s_unit_id,m.custom_path,m.custom_value FROM a_sys_nunit_item m"
            strSQL1 &= " INNER JOIN groups_power t1 ON m.s_unitem_id = t1.s_unitem_id"
            strSQL1 &= " inner join unit_groups t2 on t1.group_id=t2.group_id and CHARINDEX(t2.unit_id, '" & TransPara.TransP("Userunit") & "')>0 "
            strSQL1 &= " WHERE m.s_system_id = '" & sender.name & "'"
            strSQL1 &= " AND m.s_unit_id = '" & bmS.Current("s_unit_id") & "'"
            strSQL1 &= " ORDER BY m.sort, m.s_unitem_id ASC"

            If TransPara.TransP("isadmin") = "True" Then
                strSQL1 = "SELECT DISTINCT m.s_unitem_id, m.s_item_name,m.sort,m.s_system_id,m.s_unit_id,m.custom_path,m.custom_value FROM a_sys_nunit_item m"
                strSQL1 &= " WHERE m.s_system_id = '" & sender.name & "'"
                strSQL1 &= " AND m.s_unit_id = '" & bmS.Current("s_unit_id") & "'"
            End If

            myDatasetS = openmember(DNS_SYS, "a_sys_nunit_item", strSQL1)
            bmT = Me.BindingContext(myDatasetS, "a_sys_nunit_item")

            For ii = 0 To bmT.Count - 1
                bmT.Position = ii
                Dim submenu As ToolStripMenuItem = New ToolStripMenuItem
                If IsDBNull(bmT.Current("custom_value")) Then
                    submenu.Name = bmT.Current("s_unitem_id")
                    submenu.Text = bmT.Current("s_item_name") + bmT.Current("s_unitem_id")
                Else

                    submenu.Name = bmT.Current("custom_value")
                    submenu.Text = bmT.Current("s_item_name") + bmT.Current("custom_value")
                End If
                'submenu.Name = bmT.Current("s_unitem_id")
                'submenu.Text = bmT.Current("s_item_name") + bmT.Current("s_unitem_id")
                rootmenu.DropDownItems.Add(submenu)
                AddHandler submenu.Click, AddressOf Menuitem_Click
            Next

            MenuStrip.Items.Add(rootmenu)
        Next
    End Sub

    Private Sub RunSQL_Click(sender As Object, e As EventArgs)

        Dim path As String = Application.StartupPath + "\INI\Z01ACCTable.txt"
        Dim objReader As New StreamReader(path)

        Dim sLine As String = ""
        Dim sSql As String = ""
        'Do
        '    sLine = objReader.ReadLine()
        '    If Not sLine Is Nothing Then

        '        If sLine.Trim = "GO" Then
        '            runsql(DNS_ACC, sSql)
        '            sSql = ""
        '        Else
        '            sSql = sSql + sLine + vbNewLine
        '        End If
        '    End If
        'Loop Until sLine Is Nothing
        objReader.Close()

        path = Application.StartupPath + "\INI\Z02SYSTable.txt"
        Dim objReader1 As New StreamReader(path)

        sLine = ""
        sSql = ""
        Do
            sLine = objReader1.ReadLine()
            If Not sLine Is Nothing Then

                If sLine.Trim = "GO" Then
                    runsql(DNS_SYS, sSql)
                    sSql = ""
                Else
                    sSql = sSql + sLine + vbNewLine
                End If
            End If
        Loop Until sLine Is Nothing
        objReader1.Close()
    End Sub

    '狀態列
    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        'txtDate.Text = NowDate() & "　" & NowTime() '小時鐘
        'If Me.MdiChildren.count = 0 Then
        '    Me.Panel1.Visible = True
        'End If
    End Sub
#End Region

#Region "功能項目"

#Region "視窗"
    Private Sub W01N0111_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub
    Private Sub W01N0112_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub
    Private Sub W01N0113_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub
#End Region
#End Region

    Public Sub Menuitem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sName = CType(sender, ToolStripMenuItem)
        'Dim obj As Object = Activator.CreateInstance(Type.GetType(sName.Name))
        'Dim f As Form = CType(obj, Form)
        SetSysDefPrinter(TransPara.TransP("DefaultPrint"))

        Dim strCreatedFromButton As String = sName.Name

        Select Case sName.Name
            Case "AC010"
                SetSysDefPrinter(TransPara.TransP("ShipPrint"))
                TransPara.TransP("ac010kind") = "開立傳票"
            Case "AC011"
                SetSysDefPrinter(TransPara.TransP("ShipPrint"))
                TransPara.TransP("ac010kind") = "修改傳票"
                strCreatedFromButton = "AC010"
            Case "AC030"
                SetSysDefPrinter(TransPara.TransP("ShipPrint"))
                TransPara.TransP("ac010kind") = "開立傳票"
            Case "AC031"
                SetSysDefPrinter(TransPara.TransP("ShipPrint"))
                TransPara.TransP("ac010kind") = "修改傳票"
                strCreatedFromButton = "AC030"
            Case "BG0301"
                strCreatedFromButton = "BG030"
            Case "BUD_PSNAME"
                strCreatedFromButton = "PSNAME"
            Case "FAC010"
                TransPara.TransP("ac010kind") = "開立傳票"
            Case "FAC011"
                TransPara.TransP("ac010kind") = "修改傳票"
                strCreatedFromButton = "FAC010"
            Case "FAC030"
                TransPara.TransP("ac010kind") = "開立傳票"
            Case "FAC031"
                TransPara.TransP("ac010kind") = "修改傳票"
                strCreatedFromButton = "FAC030"
        End Select



        Dim f As New Form
        f = DirectCast(CreateObjectInstance(strCreatedFromButton), Form)
        'f.MdiParent = Me
        f.Show()
        f.StartPosition = Me.StartPosition
        f.Text = sName.Text
        'Me.Panel1.Visible = False

    End Sub
    Public Function CreateObjectInstance(ByVal objectName As String) As Object
        Dim obj As Object
        Try
            If objectName.LastIndexOf(".") = -1 Then
                objectName = Reflection.Assembly.GetEntryAssembly.GetName.Name & "." & objectName
            End If

            obj = Reflection.Assembly.GetEntryAssembly.CreateInstance(objectName)

        Catch ex As Exception
            obj = Nothing
        End Try
        Return obj

    End Function

    

    Private Sub CBShipPrint_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles CBShipPrint.SelectedIndexChanged, CBDefaultPrint.SelectedIndexChanged
        If CType(sender, ComboBox).Name = "CBShipPrint" Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "ShipPrint", CType(sender, ComboBox).Text)
            TransPara.TransP("ShipPrint") = CType(sender, ComboBox).Text
        End If

        If CType(sender, ComboBox).Name = "CBDefaultPrint" Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\JBC\FitPrint", "DefaultPrint", CType(sender, ComboBox).Text)
            TransPara.TransP("DefaultPrint") = CType(sender, ComboBox).Text
        End If
        'SetSysDefPrinter(CBShipPrint.Text) ' 設定系統預設印表機 
        ' 或 SetAppDefPrinter("HP LaserJet 1100 (MS)") ' 設定應用程式預設印表機 
        'MsgBox("您已將 " & CBShipPrint.Text & " 設為預設印表機ㄌ")
    End Sub
End Class