<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ACF070
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
        Me.vxtEndNo = New System.Windows.Forms.MaskedTextBox()
        Me.vxtStartNo = New System.Windows.Forms.MaskedTextBox()
        Me.RecMove1 = New LAEACC_AP.RecMove()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.rdbdate = New System.Windows.Forms.RadioButton()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.vxtAccno = New System.Windows.Forms.MaskedTextBox()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.lblSumDebit = New System.Windows.Forms.Label()
        Me.lblSumCredit = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCrAmt3 = New System.Windows.Forms.TextBox()
        Me.txtDeAmt3 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCrAmt2 = New System.Windows.Forms.TextBox()
        Me.txtDeAmt2 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCrAmt1 = New System.Windows.Forms.TextBox()
        Me.lblAccname = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblkey = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDeAmt1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'vxtEndNo
        '
        Me.vxtEndNo.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtEndNo.Location = New System.Drawing.Point(585, 7)
        Me.vxtEndNo.Mask = "#-####"
        Me.vxtEndNo.Name = "vxtEndNo"
        Me.vxtEndNo.Size = New System.Drawing.Size(77, 27)
        Me.vxtEndNo.TabIndex = 131
        '
        'vxtStartNo
        '
        Me.vxtStartNo.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtStartNo.Location = New System.Drawing.Point(459, 7)
        Me.vxtStartNo.Mask = "#-####"
        Me.vxtStartNo.Name = "vxtStartNo"
        Me.vxtStartNo.Size = New System.Drawing.Size(78, 27)
        Me.vxtStartNo.TabIndex = 130
        '
        'RecMove1
        '
        Me.RecMove1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RecMove1.FilesPara = Nothing
        Me.RecMove1.GenInsFunc = "() values ()"
        Me.RecMove1.genupdfunc = ""
        Me.RecMove1.Location = New System.Drawing.Point(0, 547)
        Me.RecMove1.Name = "RecMove1"
        Me.RecMove1.Setds = Nothing
        Me.RecMove1.Setpos = 0
        Me.RecMove1.Size = New System.Drawing.Size(984, 39)
        Me.RecMove1.TabIndex = 35
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.Location = New System.Drawing.Point(907, 7)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 32)
        Me.btnExit.TabIndex = 34
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label23.Location = New System.Drawing.Point(555, 7)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(24, 23)
        Me.Label23.TabIndex = 33
        Me.Label23.Text = "訖"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Controls.Add(Me.rdbdate)
        Me.GroupBox1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(683, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 48)
        Me.GroupBox1.TabIndex = 31
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "排序"
        '
        'RadioButton1
        '
        Me.RadioButton1.Location = New System.Drawing.Point(80, 16)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(64, 22)
        Me.RadioButton1.TabIndex = 6
        Me.RadioButton1.Text = "科目"
        '
        'rdbdate
        '
        Me.rdbdate.Checked = True
        Me.rdbdate.Location = New System.Drawing.Point(8, 16)
        Me.rdbdate.Name = "rdbdate"
        Me.rdbdate.Size = New System.Drawing.Size(64, 22)
        Me.rdbdate.TabIndex = 5
        Me.rdbdate.TabStop = True
        Me.rdbdate.Text = "日期"
        '
        'BtnSearch
        '
        Me.BtnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnSearch.Location = New System.Drawing.Point(843, 7)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(56, 32)
        Me.BtnSearch.TabIndex = 30
        Me.BtnSearch.Text = "查詢"
        Me.BtnSearch.UseVisualStyleBackColor = False
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 586)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(984, 25)
        Me.StatusBar1.TabIndex = 28
        Me.StatusBar1.Text = "StatusBar1"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.Location = New System.Drawing.Point(355, 7)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(112, 23)
        Me.Label15.TabIndex = 32
        Me.Label15.Text = "會計科目起值"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDate.Location = New System.Drawing.Point(219, 7)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.ShowUpDown = True
        Me.dtpEndDate.Size = New System.Drawing.Size(120, 27)
        Me.dtpEndDate.TabIndex = 26
        Me.dtpEndDate.Value = New Date(2004, 1, 3, 0, 0, 0, 0)
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CausesValidation = False
        Me.dtpStartDate.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartDate.Location = New System.Drawing.Point(51, 7)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.ShowUpDown = True
        Me.dtpStartDate.Size = New System.Drawing.Size(120, 27)
        Me.dtpStartDate.TabIndex = 25
        Me.dtpStartDate.Value = New Date(2004, 1, 3, 0, 0, 0, 0)
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.Location = New System.Drawing.Point(179, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 23)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "訖日"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 23)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "起日"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(3, 55)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(952, 420)
        Me.TabControl1.TabIndex = 27
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGrid1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(944, 390)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "多筆瀏覽"
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(0, 0)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(944, 390)
        Me.DataGrid1.TabIndex = 1
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGrid1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "acf070"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "日期"
        Me.DataGridTextBoxColumn1.MappingName = "Date_2"
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "科目"
        Me.DataGridTextBoxColumn2.MappingName = "Accno"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 50
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "會計科目名稱"
        Me.DataGridTextBoxColumn3.MappingName = "Accname"
        Me.DataGridTextBoxColumn3.Width = 110
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn4.Format = "###,###,###,###.00"
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "借方(銀行存款)    "
        Me.DataGridTextBoxColumn4.MappingName = "DeAmt1"
        Me.DataGridTextBoxColumn4.Width = 110
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn5.Format = "###,###,###,###.00"
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "借方(專戶存款)  "
        Me.DataGridTextBoxColumn5.MappingName = "DeAmt2"
        Me.DataGridTextBoxColumn5.Width = 110
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn6.Format = "###,###,###,###.00"
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "借方(轉帳)  "
        Me.DataGridTextBoxColumn6.MappingName = "DeAmt3"
        Me.DataGridTextBoxColumn6.Width = 110
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn7.Format = "###,###,###,###.00"
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "貸方(銀行存款) "
        Me.DataGridTextBoxColumn7.MappingName = "CrAmt1"
        Me.DataGridTextBoxColumn7.Width = 110
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn8.Format = "###,###,###,###.00"
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "貸方(專戶存款) "
        Me.DataGridTextBoxColumn8.MappingName = "CrAmt2"
        Me.DataGridTextBoxColumn8.Width = 110
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn9.Format = "###,###,###,###.00"
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "貸方(轉帳)  "
        Me.DataGridTextBoxColumn9.MappingName = "CrAmt3"
        Me.DataGridTextBoxColumn9.Width = 110
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.vxtAccno)
        Me.TabPage2.Controls.Add(Me.dtpDate)
        Me.TabPage2.Controls.Add(Me.lblSumDebit)
        Me.TabPage2.Controls.Add(Me.lblSumCredit)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.txtCrAmt3)
        Me.TabPage2.Controls.Add(Me.txtDeAmt3)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.txtCrAmt2)
        Me.TabPage2.Controls.Add(Me.txtDeAmt2)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.txtCrAmt1)
        Me.TabPage2.Controls.Add(Me.lblAccname)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.lblkey)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.txtDeAmt1)
        Me.TabPage2.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(944, 390)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "單筆明細"
        '
        'vxtAccno
        '
        Me.vxtAccno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccno.Location = New System.Drawing.Point(175, 100)
        Me.vxtAccno.Mask = "#-####"
        Me.vxtAccno.Name = "vxtAccno"
        Me.vxtAccno.Size = New System.Drawing.Size(78, 27)
        Me.vxtAccno.TabIndex = 131
        '
        'dtpDate
        '
        Me.dtpDate.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(176, 56)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.ShowUpDown = True
        Me.dtpDate.Size = New System.Drawing.Size(136, 30)
        Me.dtpDate.TabIndex = 8
        Me.dtpDate.Value = New Date(2004, 1, 3, 0, 0, 0, 0)
        '
        'lblSumDebit
        '
        Me.lblSumDebit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSumDebit.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblSumDebit.ForeColor = System.Drawing.Color.Red
        Me.lblSumDebit.Location = New System.Drawing.Point(168, 336)
        Me.lblSumDebit.Name = "lblSumDebit"
        Me.lblSumDebit.Size = New System.Drawing.Size(192, 23)
        Me.lblSumDebit.TabIndex = 64
        Me.lblSumDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSumCredit
        '
        Me.lblSumCredit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSumCredit.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblSumCredit.ForeColor = System.Drawing.Color.Red
        Me.lblSumCredit.Location = New System.Drawing.Point(384, 336)
        Me.lblSumCredit.Name = "lblSumCredit"
        Me.lblSumCredit.Size = New System.Drawing.Size(184, 23)
        Me.lblSumCredit.TabIndex = 63
        Me.lblSumCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(80, 336)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 23)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "合    計"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCrAmt3
        '
        Me.txtCrAmt3.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCrAmt3.Location = New System.Drawing.Point(384, 280)
        Me.txtCrAmt3.MaxLength = 20
        Me.txtCrAmt3.Name = "txtCrAmt3"
        Me.txtCrAmt3.Size = New System.Drawing.Size(184, 30)
        Me.txtCrAmt3.TabIndex = 15
        Me.txtCrAmt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDeAmt3
        '
        Me.txtDeAmt3.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtDeAmt3.Location = New System.Drawing.Point(176, 280)
        Me.txtDeAmt3.MaxLength = 20
        Me.txtDeAmt3.Name = "txtDeAmt3"
        Me.txtDeAmt3.Size = New System.Drawing.Size(184, 30)
        Me.txtDeAmt3.TabIndex = 14
        Me.txtDeAmt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(88, 280)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 23)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "轉    帳"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCrAmt2
        '
        Me.txtCrAmt2.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCrAmt2.Location = New System.Drawing.Point(384, 224)
        Me.txtCrAmt2.MaxLength = 20
        Me.txtCrAmt2.Name = "txtCrAmt2"
        Me.txtCrAmt2.Size = New System.Drawing.Size(184, 30)
        Me.txtCrAmt2.TabIndex = 13
        Me.txtCrAmt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDeAmt2
        '
        Me.txtDeAmt2.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtDeAmt2.Location = New System.Drawing.Point(176, 224)
        Me.txtDeAmt2.MaxLength = 20
        Me.txtDeAmt2.Name = "txtDeAmt2"
        Me.txtDeAmt2.Size = New System.Drawing.Size(184, 30)
        Me.txtDeAmt2.TabIndex = 12
        Me.txtDeAmt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.Location = New System.Drawing.Point(80, 224)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 23)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "專戶存款"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(384, 144)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(176, 23)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "當日貸方總額"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(192, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 23)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "當日借方總額"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCrAmt1
        '
        Me.txtCrAmt1.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCrAmt1.Location = New System.Drawing.Point(384, 168)
        Me.txtCrAmt1.MaxLength = 20
        Me.txtCrAmt1.Name = "txtCrAmt1"
        Me.txtCrAmt1.Size = New System.Drawing.Size(184, 30)
        Me.txtCrAmt1.TabIndex = 11
        Me.txtCrAmt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblAccname
        '
        Me.lblAccname.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblAccname.ForeColor = System.Drawing.Color.Green
        Me.lblAccname.Location = New System.Drawing.Point(256, 104)
        Me.lblAccname.Name = "lblAccname"
        Me.lblAccname.Size = New System.Drawing.Size(328, 23)
        Me.lblAccname.TabIndex = 19
        Me.lblAccname.Text = "lblAccname"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.Location = New System.Drawing.Point(104, 56)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 24)
        Me.Label10.TabIndex = 17
        Me.Label10.Tag = ""
        Me.Label10.Text = "日期"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblkey
        '
        Me.lblkey.Location = New System.Drawing.Point(784, 8)
        Me.lblkey.Name = "lblkey"
        Me.lblkey.Size = New System.Drawing.Size(80, 23)
        Me.lblkey.TabIndex = 15
        Me.lblkey.Text = "lblkey"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.Location = New System.Drawing.Point(104, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "科目"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(80, 168)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "銀行存款"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDeAmt1
        '
        Me.txtDeAmt1.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtDeAmt1.Location = New System.Drawing.Point(176, 168)
        Me.txtDeAmt1.MaxLength = 20
        Me.txtDeAmt1.Name = "txtDeAmt1"
        Me.txtDeAmt1.Size = New System.Drawing.Size(184, 30)
        Me.txtDeAmt1.TabIndex = 10
        Me.txtDeAmt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ACF070
        '
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.vxtEndNo)
        Me.Controls.Add(Me.vxtStartNo)
        Me.Controls.Add(Me.RecMove1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.dtpEndDate)
        Me.Controls.Add(Me.dtpStartDate)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Name = "ACF070"
        Me.GroupBox1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSumDebit As System.Windows.Forms.Label
    Friend WithEvents lblSumCredit As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCrAmt3 As System.Windows.Forms.TextBox
    Friend WithEvents txtDeAmt3 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCrAmt2 As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbdate As System.Windows.Forms.RadioButton
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDeAmt2 As System.Windows.Forms.TextBox
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCrAmt1 As System.Windows.Forms.TextBox
    Friend WithEvents lblAccname As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblkey As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDeAmt1 As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents RecMove1 As LAEACC_AP.RecMove
    Friend WithEvents vxtAccno As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtEndNo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtStartNo As System.Windows.Forms.MaskedTextBox

End Class
