<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PAY030
    Inherits LAEACC_AP.fmBase1

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意:  以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.lblDate_2 = New System.Windows.Forms.Label()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblAct_amt = New System.Windows.Forms.Label()
        Me.lblNo1 = New System.Windows.Forms.Label()
        Me.lblBank = New System.Windows.Forms.Label()
        Me.txtChkname = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblMsgDate = New System.Windows.Forms.Label()
        Me.btnDate_2 = New System.Windows.Forms.Button()
        Me.dtpDate_2 = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtChkNo = New System.Windows.Forms.TextBox()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboName = New System.Windows.Forms.ComboBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.lblTotAmt = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnSureNo = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNo1 = New System.Windows.Forms.TextBox()
        Me.RecMove1 = New LAEACC_AP.RecMove()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(104, 550)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(544, 23)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "請輸入製票後(可多張),無誤,按記入帳冊,才能入帳"
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "支款編號"
        Me.DataGridTextBoxColumn5.MappingName = "no_2_no"
        Me.DataGridTextBoxColumn5.Width = 60
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "銀行"
        Me.DataGridTextBoxColumn4.MappingName = "bank"
        Me.DataGridTextBoxColumn4.Width = 50
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn3.Format = "###,###,###.##"
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "實收數"
        Me.DataGridTextBoxColumn3.MappingName = "act_amt"
        Me.DataGridTextBoxColumn3.Width = 120
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "摘要"
        Me.DataGridTextBoxColumn2.MappingName = "remark"
        Me.DataGridTextBoxColumn2.Width = 450
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "傳票號"
        Me.DataGridTextBoxColumn1.MappingName = "no_1_no"
        Me.DataGridTextBoxColumn1.Width = 60
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGrid1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "acf010s"
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(0, 64)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ReadOnly = True
        Me.DataGrid1.Size = New System.Drawing.Size(768, 192)
        Me.DataGrid1.TabIndex = 34
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'lblDate_2
        '
        Me.lblDate_2.Location = New System.Drawing.Point(8, 8)
        Me.lblDate_2.Name = "lblDate_2"
        Me.lblDate_2.Size = New System.Drawing.Size(128, 23)
        Me.lblDate_2.TabIndex = 74
        '
        'lblBankName
        '
        Me.lblBankName.Location = New System.Drawing.Point(392, 272)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(184, 48)
        Me.lblBankName.TabIndex = 73
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(80, 448)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(448, 23)
        Me.lblRemark.TabIndex = 72
        Me.lblRemark.Text = "lblRemark"
        Me.lblRemark.Visible = False
        '
        'lblAct_amt
        '
        Me.lblAct_amt.Location = New System.Drawing.Point(536, 448)
        Me.lblAct_amt.Name = "lblAct_amt"
        Me.lblAct_amt.Size = New System.Drawing.Size(152, 23)
        Me.lblAct_amt.TabIndex = 71
        Me.lblAct_amt.Text = "lblAct_amt"
        Me.lblAct_amt.Visible = False
        '
        'lblNo1
        '
        Me.lblNo1.Location = New System.Drawing.Point(0, 448)
        Me.lblNo1.Name = "lblNo1"
        Me.lblNo1.Size = New System.Drawing.Size(72, 23)
        Me.lblNo1.TabIndex = 70
        Me.lblNo1.Text = "lblNo1"
        Me.lblNo1.Visible = False
        '
        'lblBank
        '
        Me.lblBank.Location = New System.Drawing.Point(704, 448)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(56, 23)
        Me.lblBank.TabIndex = 69
        Me.lblBank.Text = "lblBank"
        Me.lblBank.Visible = False
        '
        'txtChkname
        '
        Me.txtChkname.Location = New System.Drawing.Point(112, 360)
        Me.txtChkname.MaxLength = 25
        Me.txtChkname.Name = "txtChkname"
        Me.txtChkname.Size = New System.Drawing.Size(456, 29)
        Me.txtChkname.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(24, 408)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 23)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = "摘要："
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(112, 408)
        Me.txtRemark.MaxLength = 50
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(648, 29)
        Me.txtRemark.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(24, 360)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 23)
        Me.Label1.TabIndex = 67
        Me.Label1.Text = "受款人："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(984, 611)
        Me.TabControl1.TabIndex = 10
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.RecMove1)
        Me.TabPage1.Controls.Add(Me.lblDate_2)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.lblBankName)
        Me.TabPage1.Controls.Add(Me.lblRemark)
        Me.TabPage1.Controls.Add(Me.lblAct_amt)
        Me.TabPage1.Controls.Add(Me.lblNo1)
        Me.TabPage1.Controls.Add(Me.lblBank)
        Me.TabPage1.Controls.Add(Me.txtChkname)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtRemark)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtChkNo)
        Me.TabPage1.Controls.Add(Me.btnFinish)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.cboName)
        Me.TabPage1.Controls.Add(Me.btnExit)
        Me.TabPage1.Controls.Add(Me.DataGrid1)
        Me.TabPage1.Controls.Add(Me.lblMsg)
        Me.TabPage1.Controls.Add(Me.lblTotAmt)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.btnSureNo)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.txtNo1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(976, 578)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "收入傳票記帳"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblMsgDate)
        Me.Panel1.Controls.Add(Me.btnDate_2)
        Me.Panel1.Controls.Add(Me.dtpDate_2)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(200, 184)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 208)
        Me.Panel1.TabIndex = 60
        '
        'lblMsgDate
        '
        Me.lblMsgDate.Location = New System.Drawing.Point(16, 136)
        Me.lblMsgDate.Name = "lblMsgDate"
        Me.lblMsgDate.Size = New System.Drawing.Size(336, 56)
        Me.lblMsgDate.TabIndex = 61
        Me.lblMsgDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDate_2
        '
        Me.btnDate_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDate_2.Location = New System.Drawing.Point(136, 96)
        Me.btnDate_2.Name = "btnDate_2"
        Me.btnDate_2.Size = New System.Drawing.Size(75, 32)
        Me.btnDate_2.TabIndex = 60
        Me.btnDate_2.Text = "確定"
        Me.btnDate_2.UseVisualStyleBackColor = False
        '
        'dtpDate_2
        '
        Me.dtpDate_2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate_2.Location = New System.Drawing.Point(144, 48)
        Me.dtpDate_2.Name = "dtpDate_2"
        Me.dtpDate_2.Size = New System.Drawing.Size(160, 29)
        Me.dtpDate_2.TabIndex = 1
        Me.dtpDate_2.Value = New Date(2004, 6, 29, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Green
        Me.Label5.Location = New System.Drawing.Point(8, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 24)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "收款日期："
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChkNo
        '
        Me.txtChkNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtChkNo.Location = New System.Drawing.Point(112, 312)
        Me.txtChkNo.MaxLength = 10
        Me.txtChkNo.Name = "txtChkNo"
        Me.txtChkNo.Size = New System.Drawing.Size(112, 29)
        Me.txtChkNo.TabIndex = 3
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.Red
        Me.btnFinish.Enabled = False
        Me.btnFinish.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnFinish.Location = New System.Drawing.Point(368, 16)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(104, 32)
        Me.btnFinish.TabIndex = 2
        Me.btnFinish.Text = "記入帳冊"
        Me.btnFinish.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(24, 320)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 24)
        Me.Label11.TabIndex = 66
        Me.Label11.Text = "支票號碼："
        '
        'cboName
        '
        Me.cboName.Location = New System.Drawing.Point(112, 360)
        Me.cboName.MaxDropDownItems = 20
        Me.cboName.Name = "cboName"
        Me.cboName.Size = New System.Drawing.Size(472, 28)
        Me.cboName.Sorted = True
        Me.cboName.TabIndex = 65
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.Location = New System.Drawing.Point(688, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 32)
        Me.btnExit.TabIndex = 35
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblMsg
        '
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(496, 24)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(176, 23)
        Me.lblMsg.TabIndex = 33
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotAmt
        '
        Me.lblTotAmt.ForeColor = System.Drawing.Color.Red
        Me.lblTotAmt.Location = New System.Drawing.Point(648, 264)
        Me.lblTotAmt.Name = "lblTotAmt"
        Me.lblTotAmt.Size = New System.Drawing.Size(112, 23)
        Me.lblTotAmt.TabIndex = 30
        Me.lblTotAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(592, 264)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(48, 23)
        Me.Label18.TabIndex = 29
        Me.Label18.Text = "總額"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSureNo
        '
        Me.btnSureNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSureNo.Enabled = False
        Me.btnSureNo.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSureNo.Location = New System.Drawing.Point(296, 24)
        Me.btnSureNo.Name = "btnSureNo"
        Me.btnSureNo.Size = New System.Drawing.Size(56, 32)
        Me.btnSureNo.TabIndex = 8
        Me.btnSureNo.Text = " 確定"
        Me.btnSureNo.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(80, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 23)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "請輸入製票編號"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNo1
        '
        Me.txtNo1.AccessibleDescription = ""
        Me.txtNo1.Enabled = False
        Me.txtNo1.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtNo1.Location = New System.Drawing.Point(216, 24)
        Me.txtNo1.MaxLength = 5
        Me.txtNo1.Name = "txtNo1"
        Me.txtNo1.Size = New System.Drawing.Size(80, 30)
        Me.txtNo1.TabIndex = 0
        '
        'RecMove1
        '
        Me.RecMove1.FilesPara = Nothing
        Me.RecMove1.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.RecMove1.GenInsFunc = "() values ()"
        Me.RecMove1.genupdfunc = ""
        Me.RecMove1.Location = New System.Drawing.Point(4, 264)
        Me.RecMove1.Name = "RecMove1"
        Me.RecMove1.Setds = Nothing
        Me.RecMove1.Setpos = 0
        Me.RecMove1.Size = New System.Drawing.Size(342, 29)
        Me.RecMove1.TabIndex = 75
        '
        'PAY030
        '
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "PAY030"
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Protected WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents lblDate_2 As System.Windows.Forms.Label
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents lblAct_amt As System.Windows.Forms.Label
    Friend WithEvents lblNo1 As System.Windows.Forms.Label
    Friend WithEvents lblBank As System.Windows.Forms.Label
    Friend WithEvents txtChkname As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblMsgDate As System.Windows.Forms.Label
    Friend WithEvents btnDate_2 As System.Windows.Forms.Button
    Friend WithEvents dtpDate_2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtChkNo As System.Windows.Forms.TextBox
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboName As System.Windows.Forms.ComboBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents lblTotAmt As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnSureNo As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtNo1 As System.Windows.Forms.TextBox
    Friend WithEvents RecMove1 As LAEACC_AP.RecMove

End Class
