<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PAY021
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTotAmt = New System.Windows.Forms.Label()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.txtChkNo = New System.Windows.Forms.TextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblTotamt2 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.btnSureTR = New System.Windows.Forms.Button()
        Me.lblTotNo = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblAct_Amt = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblBank = New System.Windows.Forms.Label()
        Me.label14 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCode6 = New System.Windows.Forms.TextBox()
        Me.txtCode5 = New System.Windows.Forms.TextBox()
        Me.txtCode4 = New System.Windows.Forms.TextBox()
        Me.txtCode3 = New System.Windows.Forms.TextBox()
        Me.lblNo1 = New System.Windows.Forms.Label()
        Me.cboName = New System.Windows.Forms.ComboBox()
        Me.btnSureNo = New System.Windows.Forms.Button()
        Me.lblChkTR = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnAddPsname = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtChkname = New System.Windows.Forms.TextBox()
        Me.txtCode2 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNo1 = New System.Windows.Forms.TextBox()
        Me.btnSure = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.RecMove1 = New LAEACC_AP.RecMove()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(32, 216)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 23)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "摘要："
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(32, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 23)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "受款人："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMsg
        '
        Me.lblMsg.Location = New System.Drawing.Point(440, 56)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(312, 23)
        Me.lblMsg.TabIndex = 33
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBalance
        '
        Me.lblBalance.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblBalance.ForeColor = System.Drawing.Color.Red
        Me.lblBalance.Location = New System.Drawing.Point(432, 464)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(144, 23)
        Me.lblBalance.TabIndex = 32
        Me.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(368, 464)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 23)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "帳戶餘額"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotAmt
        '
        Me.lblTotAmt.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblTotAmt.ForeColor = System.Drawing.Color.Red
        Me.lblTotAmt.Location = New System.Drawing.Point(656, 456)
        Me.lblTotAmt.Name = "lblTotAmt"
        Me.lblTotAmt.Size = New System.Drawing.Size(104, 23)
        Me.lblTotAmt.TabIndex = 30
        Me.lblTotAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBankName
        '
        Me.lblBankName.ForeColor = System.Drawing.Color.Red
        Me.lblBankName.Location = New System.Drawing.Point(120, 48)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(248, 23)
        Me.lblBankName.TabIndex = 45
        Me.lblBankName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtChkNo
        '
        Me.txtChkNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtChkNo.Location = New System.Drawing.Point(120, 120)
        Me.txtChkNo.MaxLength = 10
        Me.txtChkNo.Name = "txtChkNo"
        Me.txtChkNo.Size = New System.Drawing.Size(112, 29)
        Me.txtChkNo.TabIndex = 10
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Black
        Me.btnExit.Location = New System.Drawing.Point(696, 8)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(56, 32)
        Me.btnExit.TabIndex = 35
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblTotamt2
        '
        Me.lblTotamt2.ForeColor = System.Drawing.Color.Red
        Me.lblTotamt2.Location = New System.Drawing.Point(128, 88)
        Me.lblTotamt2.Name = "lblTotamt2"
        Me.lblTotamt2.Size = New System.Drawing.Size(112, 23)
        Me.lblTotamt2.TabIndex = 43
        Me.lblTotamt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(32, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 23)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "支票金額："
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.Red
        Me.btnFinish.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnFinish.Location = New System.Drawing.Point(248, 264)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(64, 32)
        Me.btnFinish.TabIndex = 13
        Me.btnFinish.Text = "確定"
        Me.btnFinish.UseVisualStyleBackColor = False
        Me.btnFinish.Visible = False
        '
        'btnSureTR
        '
        Me.btnSureTR.AccessibleDescription = ""
        Me.btnSureTR.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSureTR.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSureTR.ForeColor = System.Drawing.Color.Yellow
        Me.btnSureTR.Location = New System.Drawing.Point(440, 8)
        Me.btnSureTR.Name = "btnSureTR"
        Me.btnSureTR.Size = New System.Drawing.Size(120, 40)
        Me.btnSureTR.TabIndex = 38
        Me.btnSureTR.Text = "開立電子支票"
        Me.btnSureTR.UseVisualStyleBackColor = False
        Me.btnSureTR.Visible = False
        '
        'lblTotNo
        '
        Me.lblTotNo.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblTotNo.ForeColor = System.Drawing.Color.Red
        Me.lblTotNo.Location = New System.Drawing.Point(648, 480)
        Me.lblTotNo.Name = "lblTotNo"
        Me.lblTotNo.Size = New System.Drawing.Size(104, 23)
        Me.lblTotNo.TabIndex = 37
        Me.lblTotNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(584, 480)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 23)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "傳票張數"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(0, 104)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(768, 352)
        Me.DataGrid1.TabIndex = 34
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGrid1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "acf010s"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "傳票號"
        Me.DataGridTextBoxColumn1.MappingName = "no_1_no"
        Me.DataGridTextBoxColumn1.Width = 60
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "摘要"
        Me.DataGridTextBoxColumn2.MappingName = "remark"
        Me.DataGridTextBoxColumn2.Width = 500
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn3.Format = "###,###,###"
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "實付數"
        Me.DataGridTextBoxColumn3.MappingName = "act_amt"
        Me.DataGridTextBoxColumn3.Width = 120
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "銀行"
        Me.DataGridTextBoxColumn4.MappingName = "bank"
        Me.DataGridTextBoxColumn4.Width = 50
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(32, 128)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 24)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "支票號碼："
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(584, 456)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(64, 23)
        Me.Label18.TabIndex = 29
        Me.Label18.Text = "支票總額"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(24, 56)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 23)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "傳票"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAct_Amt
        '
        Me.lblAct_Amt.ForeColor = System.Drawing.Color.Red
        Me.lblAct_Amt.Location = New System.Drawing.Point(224, 56)
        Me.lblAct_Amt.Name = "lblAct_Amt"
        Me.lblAct_Amt.Size = New System.Drawing.Size(112, 23)
        Me.lblAct_Amt.TabIndex = 21
        Me.lblAct_Amt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(152, 56)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 23)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "金額"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRemark
        '
        Me.lblRemark.ForeColor = System.Drawing.Color.Red
        Me.lblRemark.Location = New System.Drawing.Point(96, 80)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(592, 23)
        Me.lblRemark.TabIndex = 19
        Me.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(24, 80)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 23)
        Me.Label15.TabIndex = 18
        Me.Label15.Text = "摘要"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBank
        '
        Me.lblBank.ForeColor = System.Drawing.Color.Red
        Me.lblBank.Location = New System.Drawing.Point(392, 56)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(40, 23)
        Me.lblBank.TabIndex = 17
        Me.lblBank.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label14
        '
        Me.label14.Location = New System.Drawing.Point(336, 56)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(56, 23)
        Me.label14.TabIndex = 16
        Me.label14.Text = "銀行"
        Me.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(120, 216)
        Me.txtRemark.MaxLength = 60
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(616, 29)
        Me.txtRemark.TabIndex = 12
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(40, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 24)
        Me.Label9.TabIndex = 36
        Me.Label9.Text = "銀行："
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(896, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 56)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "內容別"
        '
        'txtCode6
        '
        Me.txtCode6.Location = New System.Drawing.Point(896, 352)
        Me.txtCode6.MaxLength = 1
        Me.txtCode6.Name = "txtCode6"
        Me.txtCode6.Size = New System.Drawing.Size(32, 29)
        Me.txtCode6.TabIndex = 27
        Me.txtCode6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCode5
        '
        Me.txtCode5.Location = New System.Drawing.Point(896, 296)
        Me.txtCode5.MaxLength = 1
        Me.txtCode5.Name = "txtCode5"
        Me.txtCode5.Size = New System.Drawing.Size(32, 29)
        Me.txtCode5.TabIndex = 25
        Me.txtCode5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCode4
        '
        Me.txtCode4.Location = New System.Drawing.Point(896, 240)
        Me.txtCode4.MaxLength = 1
        Me.txtCode4.Name = "txtCode4"
        Me.txtCode4.Size = New System.Drawing.Size(32, 29)
        Me.txtCode4.TabIndex = 23
        Me.txtCode4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCode3
        '
        Me.txtCode3.Location = New System.Drawing.Point(896, 184)
        Me.txtCode3.MaxLength = 1
        Me.txtCode3.Name = "txtCode3"
        Me.txtCode3.Size = New System.Drawing.Size(32, 29)
        Me.txtCode3.TabIndex = 21
        Me.txtCode3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblNo1
        '
        Me.lblNo1.ForeColor = System.Drawing.Color.Red
        Me.lblNo1.Location = New System.Drawing.Point(96, 56)
        Me.lblNo1.Name = "lblNo1"
        Me.lblNo1.Size = New System.Drawing.Size(40, 23)
        Me.lblNo1.TabIndex = 25
        Me.lblNo1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboName
        '
        Me.cboName.Location = New System.Drawing.Point(120, 168)
        Me.cboName.MaxDropDownItems = 20
        Me.cboName.Name = "cboName"
        Me.cboName.Size = New System.Drawing.Size(464, 28)
        Me.cboName.Sorted = True
        Me.cboName.TabIndex = 30
        '
        'btnSureNo
        '
        Me.btnSureNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSureNo.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSureNo.Location = New System.Drawing.Point(272, 16)
        Me.btnSureNo.Name = "btnSureNo"
        Me.btnSureNo.Size = New System.Drawing.Size(56, 32)
        Me.btnSureNo.TabIndex = 2
        Me.btnSureNo.Text = " 確定"
        Me.btnSureNo.UseVisualStyleBackColor = False
        '
        'lblChkTR
        '
        Me.lblChkTR.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblChkTR.ForeColor = System.Drawing.Color.Blue
        Me.lblChkTR.Location = New System.Drawing.Point(240, 120)
        Me.lblChkTR.Name = "lblChkTR"
        Me.lblChkTR.Size = New System.Drawing.Size(416, 24)
        Me.lblChkTR.TabIndex = 51
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnBack.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnBack.Location = New System.Drawing.Point(352, 264)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(64, 32)
        Me.btnBack.TabIndex = 50
        Me.btnBack.Text = "放棄"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnAddPsname
        '
        Me.btnAddPsname.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAddPsname.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnAddPsname.Location = New System.Drawing.Point(592, 160)
        Me.btnAddPsname.Name = "btnAddPsname"
        Me.btnAddPsname.Size = New System.Drawing.Size(72, 40)
        Me.btnAddPsname.TabIndex = 49
        Me.btnAddPsname.Text = "將受款人增入片語"
        Me.btnAddPsname.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.lblChkTR)
        Me.TabPage2.Controls.Add(Me.btnBack)
        Me.TabPage2.Controls.Add(Me.btnAddPsname)
        Me.TabPage2.Controls.Add(Me.txtChkname)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.txtRemark)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.lblBankName)
        Me.TabPage2.Controls.Add(Me.txtChkNo)
        Me.TabPage2.Controls.Add(Me.lblTotamt2)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.btnFinish)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.txtCode6)
        Me.TabPage2.Controls.Add(Me.txtCode5)
        Me.TabPage2.Controls.Add(Me.txtCode4)
        Me.TabPage2.Controls.Add(Me.txtCode3)
        Me.TabPage2.Controls.Add(Me.txtCode2)
        Me.TabPage2.Controls.Add(Me.cboName)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(768, 503)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "開立支票"
        Me.TabPage2.ToolTipText = "由來源傳票,完成後並列印支票"
        '
        'txtChkname
        '
        Me.txtChkname.Location = New System.Drawing.Point(120, 168)
        Me.txtChkname.MaxLength = 50
        Me.txtChkname.Name = "txtChkname"
        Me.txtChkname.Size = New System.Drawing.Size(448, 29)
        Me.txtChkname.TabIndex = 11
        '
        'txtCode2
        '
        Me.txtCode2.Location = New System.Drawing.Point(896, 128)
        Me.txtCode2.MaxLength = 1
        Me.txtCode2.Name = "txtCode2"
        Me.txtCode2.Size = New System.Drawing.Size(32, 29)
        Me.txtCode2.TabIndex = 19
        Me.txtCode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(32, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(144, 23)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "請輸入製票編號"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNo1
        '
        Me.txtNo1.Location = New System.Drawing.Point(184, 16)
        Me.txtNo1.MaxLength = 5
        Me.txtNo1.Name = "txtNo1"
        Me.txtNo1.Size = New System.Drawing.Size(80, 25)
        Me.txtNo1.TabIndex = 0
        '
        'btnSure
        '
        Me.btnSure.AccessibleDescription = ""
        Me.btnSure.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSure.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSure.ForeColor = System.Drawing.Color.Yellow
        Me.btnSure.Location = New System.Drawing.Point(344, 8)
        Me.btnSure.Name = "btnSure"
        Me.btnSure.Size = New System.Drawing.Size(80, 40)
        Me.btnSure.TabIndex = 3
        Me.btnSure.Text = "開立支票"
        Me.btnSure.UseVisualStyleBackColor = False
        Me.btnSure.Visible = False
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.RecMove1)
        Me.TabPage1.Controls.Add(Me.btnSureTR)
        Me.TabPage1.Controls.Add(Me.lblTotNo)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.btnExit)
        Me.TabPage1.Controls.Add(Me.DataGrid1)
        Me.TabPage1.Controls.Add(Me.lblMsg)
        Me.TabPage1.Controls.Add(Me.lblBalance)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.lblTotAmt)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.lblNo1)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.lblAct_Amt)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Controls.Add(Me.lblRemark)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.lblBank)
        Me.TabPage1.Controls.Add(Me.label14)
        Me.TabPage1.Controls.Add(Me.btnSureNo)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.txtNo1)
        Me.TabPage1.Controls.Add(Me.btnSure)
        Me.TabPage1.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(976, 578)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "來源傳票"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(984, 611)
        Me.TabControl1.TabIndex = 9
        '
        'RecMove1
        '
        Me.RecMove1.FilesPara = Nothing
        Me.RecMove1.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.RecMove1.GenInsFunc = "() values ()"
        Me.RecMove1.genupdfunc = ""
        Me.RecMove1.Location = New System.Drawing.Point(-1, 457)
        Me.RecMove1.Name = "RecMove1"
        Me.RecMove1.Setds = Nothing
        Me.RecMove1.Setpos = 0
        Me.RecMove1.Size = New System.Drawing.Size(377, 29)
        Me.RecMove1.TabIndex = 39
        '
        'PAY021
        '
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "PAY021"
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblTotAmt As System.Windows.Forms.Label
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents txtChkNo As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblTotamt2 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents btnSureTR As System.Windows.Forms.Button
    Friend WithEvents lblTotNo As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblAct_Amt As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblBank As System.Windows.Forms.Label
    Friend WithEvents label14 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCode6 As System.Windows.Forms.TextBox
    Friend WithEvents txtCode5 As System.Windows.Forms.TextBox
    Friend WithEvents txtCode4 As System.Windows.Forms.TextBox
    Friend WithEvents txtCode3 As System.Windows.Forms.TextBox
    Friend WithEvents lblNo1 As System.Windows.Forms.Label
    Friend WithEvents cboName As System.Windows.Forms.ComboBox
    Friend WithEvents btnSureNo As System.Windows.Forms.Button
    Friend WithEvents lblChkTR As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnAddPsname As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtChkname As System.Windows.Forms.TextBox
    Friend WithEvents txtCode2 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtNo1 As System.Windows.Forms.TextBox
    Friend WithEvents btnSure As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents RecMove1 As LAEACC_AP.RecMove

End Class
