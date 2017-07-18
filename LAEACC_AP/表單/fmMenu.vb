Public Class fmMenu
    Dim sqlstr, strSQL, strSQL1 As String
    Dim bmS, bmT As BindingManagerBase
    Dim myDatasetS As DataSet
    Dim DNS_SYS As String = INI_Read("CONFIG", "SET", "DNS_SYS")

    Private Sub fmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'sqlstr = " select * from a_sys_name  " & _
        '" order by sort"

        strSQL = "select DISTINCT c.s_system_id, d.s_system_name, d.sort "
        strSQL &= " from groups_power a "
        strSQL &= " inner join users_groups b on a.group_id=b.group_id and b.user_id = '" & TransPara.TransP("Userid") & "'"
        strSQL &= " INNER JOIN a_sys_nunit_item c on a.s_unitem_id=c.s_unitem_id"
        strSQL &= " INNER JOIN a_sys_name d ON d.s_system_id=c.s_system_id"
        strSQL &= " union "
        strSQL &= " select DISTINCT c.s_system_id, d.s_system_name, d.sort "
        strSQL &= " from groups_power a "
        strSQL &= " inner join unit_groups b on a.group_id=b.group_id and CHARINDEX(b.unit_id, '" & TransPara.TransP("Userunit") & "')>0 "
        strSQL &= " INNER JOIN a_sys_nunit_item c on a.s_unitem_id=c.s_unitem_id"
        strSQL &= " INNER JOIN a_sys_name d ON d.s_system_id=c.s_system_id"

        myDatasetS = openmember(DNS_SYS, "a_sys_name", strSQL)
        bmS = Me.BindingContext(myDatasetS, "a_sys_name")

        Dim x_pos As Integer = 250
        Dim y_pos As Integer = 70
        Dim x As Integer = 0
        Dim y As Integer = 1
        Dim FirstBt As Button

        For i = 0 To bmS.Count - 1
            bmS.Position = i
            x = x + 1
            If i Mod 3 = 0 Then
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
            Me.Controls.Add(bt)
            AddHandler bt.Click, AddressOf Me.bt_Click
        Next

        FirstBt.PerformClick()
    End Sub


    Private Sub bt_Click(sender As Object, e As EventArgs)
        fmMain.MenuStrip.Items.Clear()

        'sqlstr = " select * from a_sys_nunit where s_system_id =  '" + sender.name + "'"

        strSQL = "SELECT DISTINCT m.s_unit_id, t1.s_unit_name, t1.s_unit_css FROM a_sys_nunit_item m"
        strSQL &= " INNER JOIN a_sys_nunit t1 ON m.s_system_id = t1.s_system_id AND m.s_unit_id = t1.s_unit_id "
        strSQL &= " INNER JOIN groups_power t2 ON m.s_unitem_id = t2.s_unitem_id"
        strSQL &= " inner join users_groups t3 on t2.group_id=t3.group_id and t3.user_id = '" & TransPara.TransP("Userid") & "'"
        strSQL &= " WHERE m.s_system_id = '" & sender.name & "'"
        strSQL &= " union "
        strSQL &= " SELECT DISTINCT m.s_unit_id, t1.s_unit_name, t1.s_unit_css FROM a_sys_nunit_item m"
        strSQL &= "  INNER JOIN a_sys_nunit t1 ON m.s_system_id = t1.s_system_id AND m.s_unit_id = t1.s_unit_id "
        strSQL &= "  INNER JOIN groups_power t2 ON m.s_unitem_id = t2.s_unitem_id"
        strSQL &= "  inner join unit_groups t3 on t2.group_id=t3.group_id and CHARINDEX(t3.unit_id, '" & TransPara.TransP("Userunit") & "')>0 "
        strSQL &= "  WHERE m.s_system_id = '" & sender.name & "'"
        strSQL &= " ORDER BY m.s_unit_id"


        myDatasetS = openmember(DNS_SYS, "a_sys_nunit", strSQL)
        bmS = Me.BindingContext(myDatasetS, "a_sys_nunit")

        For i = 0 To bmS.Count - 1
            bmS.Position = i

            Dim rootmenu As ToolStripMenuItem = New ToolStripMenuItem
            rootmenu.Name = bmS.Current("s_unit_id")
            rootmenu.Text = bmS.Current("s_unit_name")

            'sqlstr = " select * from a_sys_nunit_item where s_system_id =  '" + sender.name + "' and s_unit_id= '" + bmS.Current("s_unit_id") + "' order by sort"

            strSQL1 = "SELECT DISTINCT m.s_unitem_id, m.s_item_name,m.sort,m.s_system_id,m.s_unit_id,m.custom_path,m.custom_value FROM a_sys_nunit_item m"
            strSQL1 &= " INNER JOIN groups_power t1 ON m.s_unitem_id = t1.s_unitem_id"
            strSQL1 &= " inner join users_groups t2 on t1.group_id=t2.group_id and t2.user_id = '" & TransPara.TransP("Userid") & "'"
            strSQL1 &= " WHERE m.s_system_id = '" & sender.name & "'"
            strSQL1 &= " AND m.s_unit_id = '" & bmS.Current("s_unit_id") & "'"
            strSQL1 &= " union "
            strSQL1 &= " SELECT DISTINCT m.s_unitem_id, m.s_item_name,m.sort,m.s_system_id,m.s_unit_id,m.custom_path,m.custom_value FROM a_sys_nunit_item m"
            strSQL1 &= " INNER JOIN groups_power t1 ON m.s_unitem_id = t1.s_unitem_id"
            strSQL1 &= " inner join unit_groups t2 on t1.group_id=t2.group_id and CHARINDEX(t2.unit_id, '" & TransPara.TransP("Userunit") & "')>0 "
            strSQL1 &= " WHERE m.s_system_id = '" & sender.name & "'"
            strSQL1 &= " AND m.s_unit_id = '" & bmS.Current("s_unit_id") & "'"
            strSQL1 &= " ORDER BY m.sort, m.s_unitem_id ASC"


            myDatasetS = openmember(DNS_SYS, "a_sys_nunit_item", strSQL1)
            bmT = Me.BindingContext(myDatasetS, "a_sys_nunit_item")

            For ii = 0 To bmT.Count - 1
                bmT.Position = ii
                Dim submenu As ToolStripMenuItem = New ToolStripMenuItem
                submenu.Name = bmT.Current("s_unitem_id")
                submenu.Text = bmT.Current("s_item_name")
                rootmenu.DropDownItems.Add(submenu)
                AddHandler submenu.Click, AddressOf fmMain.Menuitem_Click
            Next

            fmMain.MenuStrip.Items.Add(rootmenu)
        Next
    End Sub
   
End Class