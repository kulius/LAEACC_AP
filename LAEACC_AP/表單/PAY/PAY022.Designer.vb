<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PAY022
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
        Me.btnPay025 = New System.Windows.Forms.Button()
        Me.pnlChk = New System.Windows.Forms.Panel()
        Me.btnChkNo2 = New System.Windows.Forms.Button()
        Me.btnChkno = New System.Windows.Forms.Button()
        Me.txtBank = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblamt = New System.Windows.Forms.Label()
        Me.txtChkNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblChkname = New System.Windows.Forms.Label()
        Me.lblBankname = New System.Windows.Forms.Label()
        Me.lblDate_2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblMsgDate = New System.Windows.Forms.Label()
        Me.btnDate_2 = New System.Windows.Forms.Button()
        Me.dtpDate_2 = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.btnGiveUp = New System.Windows.Forms.Button()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.pnlChk.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPay025
        '
        Me.btnPay025.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnPay025.Enabled = False
        Me.btnPay025.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnPay025.Location = New System.Drawing.Point(440, 40)
        Me.btnPay025.Name = "btnPay025"
        Me.btnPay025.Size = New System.Drawing.Size(136, 32)
        Me.btnPay025.TabIndex = 7
        Me.btnPay025.Text = "由製票編號記帳"
        Me.btnPay025.UseVisualStyleBackColor = False
        '
        'pnlChk
        '
        Me.pnlChk.Controls.Add(Me.btnChkNo2)
        Me.pnlChk.Controls.Add(Me.btnPay025)
        Me.pnlChk.Controls.Add(Me.btnChkno)
        Me.pnlChk.Controls.Add(Me.txtBank)
        Me.pnlChk.Controls.Add(Me.Label9)
        Me.pnlChk.Controls.Add(Me.Label11)
        Me.pnlChk.Controls.Add(Me.Label2)
        Me.pnlChk.Controls.Add(Me.lblamt)
        Me.pnlChk.Controls.Add(Me.txtChkNo)
        Me.pnlChk.Controls.Add(Me.Label1)
        Me.pnlChk.Controls.Add(Me.lblChkname)
        Me.pnlChk.Controls.Add(Me.lblBankname)
        Me.pnlChk.Location = New System.Drawing.Point(120, 3)
        Me.pnlChk.Name = "pnlChk"
        Me.pnlChk.Size = New System.Drawing.Size(592, 112)
        Me.pnlChk.TabIndex = 67
        '
        'btnChkNo2
        '
        Me.btnChkNo2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnChkNo2.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnChkNo2.Location = New System.Drawing.Point(328, 40)
        Me.btnChkNo2.Name = "btnChkNo2"
        Me.btnChkNo2.Size = New System.Drawing.Size(96, 32)
        Me.btnChkNo2.TabIndex = 56
        Me.btnChkNo2.Text = "調出電子支票"
        Me.btnChkNo2.UseVisualStyleBackColor = False
        Me.btnChkNo2.Visible = False
        '
        'btnChkno
        '
        Me.btnChkno.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnChkno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnChkno.Location = New System.Drawing.Point(224, 40)
        Me.btnChkno.Name = "btnChkno"
        Me.btnChkno.Size = New System.Drawing.Size(88, 32)
        Me.btnChkno.TabIndex = 4
        Me.btnChkno.Text = "調出支票"
        Me.btnChkno.UseVisualStyleBackColor = False
        Me.btnChkno.Visible = False
        '
        'txtBank
        '
        Me.txtBank.Enabled = False
        Me.txtBank.Location = New System.Drawing.Point(104, 8)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(56, 29)
        Me.txtBank.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(24, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 24)
        Me.Label9.TabIndex = 36
        Me.Label9.Text = "銀行："
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(16, 40)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 24)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "支票號碼："
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(16, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 23)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "支票金額："
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblamt
        '
        Me.lblamt.ForeColor = System.Drawing.Color.Red
        Me.lblamt.Location = New System.Drawing.Point(112, 80)
        Me.lblamt.Name = "lblamt"
        Me.lblamt.Size = New System.Drawing.Size(112, 23)
        Me.lblamt.TabIndex = 43
        Me.lblamt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtChkNo
        '
        Me.txtChkNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtChkNo.Enabled = False
        Me.txtChkNo.Location = New System.Drawing.Point(104, 40)
        Me.txtChkNo.MaxLength = 10
        Me.txtChkNo.Name = "txtChkNo"
        Me.txtChkNo.Size = New System.Drawing.Size(112, 29)
        Me.txtChkNo.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(224, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 23)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "受款人："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblChkname
        '
        Me.lblChkname.Location = New System.Drawing.Point(304, 80)
        Me.lblChkname.Name = "lblChkname"
        Me.lblChkname.Size = New System.Drawing.Size(256, 23)
        Me.lblChkname.TabIndex = 55
        Me.lblChkname.Visible = False
        '
        'lblBankname
        '
        Me.lblBankname.Location = New System.Drawing.Point(168, 8)
        Me.lblBankname.Name = "lblBankname"
        Me.lblBankname.Size = New System.Drawing.Size(328, 23)
        Me.lblBankname.TabIndex = 54
        '
        'lblDate_2
        '
        Me.lblDate_2.Location = New System.Drawing.Point(8, 8)
        Me.lblDate_2.Name = "lblDate_2"
        Me.lblDate_2.Size = New System.Drawing.Size(144, 23)
        Me.lblDate_2.TabIndex = 75
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblDate_2)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.DataGrid1)
        Me.Panel2.Controls.Add(Me.btnGiveUp)
        Me.Panel2.Controls.Add(Me.btnFinish)
        Me.Panel2.Location = New System.Drawing.Point(96, 115)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(768, 432)
        Me.Panel2.TabIndex = 66
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblMsgDate)
        Me.Panel1.Controls.Add(Me.btnDate_2)
        Me.Panel1.Controls.Add(Me.dtpDate_2)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(216, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 208)
        Me.Panel1.TabIndex = 59
        '
        'lblMsgDate
        '
        Me.lblMsgDate.Location = New System.Drawing.Point(16, 144)
        Me.lblMsgDate.Name = "lblMsgDate"
        Me.lblMsgDate.Size = New System.Drawing.Size(328, 56)
        Me.lblMsgDate.TabIndex = 61
        Me.lblMsgDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDate_2
        '
        Me.btnDate_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDate_2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
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
        Me.Label5.Text = "領票日期："
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(0, 32)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ReadOnly = True
        Me.DataGrid1.Size = New System.Drawing.Size(760, 400)
        Me.DataGrid1.TabIndex = 60
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGrid1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "acf010s"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "製票編號"
        Me.DataGridTextBoxColumn1.MappingName = "no_1_no"
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "摘要"
        Me.DataGridTextBoxColumn2.MappingName = "remark"
        Me.DataGridTextBoxColumn2.Width = 400
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
        Me.DataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "銀行"
        Me.DataGridTextBoxColumn4.MappingName = "bank"
        Me.DataGridTextBoxColumn4.Width = 50
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "支款號"
        Me.DataGridTextBoxColumn5.MappingName = "no_2_no"
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'btnGiveUp
        '
        Me.btnGiveUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnGiveUp.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnGiveUp.Location = New System.Drawing.Point(408, 0)
        Me.btnGiveUp.Name = "btnGiveUp"
        Me.btnGiveUp.Size = New System.Drawing.Size(75, 32)
        Me.btnGiveUp.TabIndex = 5
        Me.btnGiveUp.Text = "放棄"
        Me.btnGiveUp.UseVisualStyleBackColor = False
        Me.btnGiveUp.Visible = False
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.Red
        Me.btnFinish.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnFinish.Location = New System.Drawing.Point(280, 0)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(75, 32)
        Me.btnFinish.TabIndex = 4
        Me.btnFinish.Text = "確定"
        Me.btnFinish.UseVisualStyleBackColor = False
        Me.btnFinish.Visible = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.Location = New System.Drawing.Point(792, 19)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(56, 32)
        Me.btnExit.TabIndex = 64
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblMsg
        '
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(712, 75)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(144, 32)
        Me.lblMsg.TabIndex = 65
        '
        'PAY022
        '
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.pnlChk)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblMsg)
        Me.Name = "PAY022"
        Me.pnlChk.ResumeLayout(False)
        Me.pnlChk.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPay025 As System.Windows.Forms.Button
    Friend WithEvents pnlChk As System.Windows.Forms.Panel
    Friend WithEvents btnChkNo2 As System.Windows.Forms.Button
    Friend WithEvents btnChkno As System.Windows.Forms.Button
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblamt As System.Windows.Forms.Label
    Friend WithEvents txtChkNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblChkname As System.Windows.Forms.Label
    Friend WithEvents lblBankname As System.Windows.Forms.Label
    Friend WithEvents lblDate_2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblMsgDate As System.Windows.Forms.Label
    Friend WithEvents btnDate_2 As System.Windows.Forms.Button
    Friend WithEvents dtpDate_2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnGiveUp As System.Windows.Forms.Button
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label

End Class
