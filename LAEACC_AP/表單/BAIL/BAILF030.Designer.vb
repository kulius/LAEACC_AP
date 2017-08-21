<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BAILF030
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
        Me.gpbInq = New System.Windows.Forms.GroupBox()
        Me.txtCopInq = New System.Windows.Forms.TextBox()
        Me.rdbCopInq = New System.Windows.Forms.RadioButton()
        Me.txtEngNameInq = New System.Windows.Forms.TextBox()
        Me.rdbEngNameInq = New System.Windows.Forms.RadioButton()
        Me.dtpRpdateStop = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dtpRpdateStart = New System.Windows.Forms.DateTimePicker()
        Me.rdbRpdateInq = New System.Windows.Forms.RadioButton()
        Me.rdbEngnoInq = New System.Windows.Forms.RadioButton()
        Me.txtEngnoStop = New System.Windows.Forms.TextBox()
        Me.txtEngnoStart = New System.Windows.Forms.TextBox()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.btnInq = New System.Windows.Forms.Button()
        Me.chkKeepDate = New System.Windows.Forms.CheckBox()
        Me.dtpKeepDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkTest2Date = New System.Windows.Forms.CheckBox()
        Me.chkOpenDate = New System.Windows.Forms.CheckBox()
        Me.txtEngname = New System.Windows.Forms.TextBox()
        Me.txtCop = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.txtEyear = New System.Windows.Forms.TextBox()
        Me.dtpTest2Date = New System.Windows.Forms.DateTimePicker()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.dtpOpenDate = New System.Windows.Forms.DateTimePicker()
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
        Me.txtIdno = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEngno = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RecMove1 = New LAEACC_AP.RecMove()
        Me.gpbInq.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpbInq
        '
        Me.gpbInq.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gpbInq.Controls.Add(Me.txtCopInq)
        Me.gpbInq.Controls.Add(Me.rdbCopInq)
        Me.gpbInq.Controls.Add(Me.txtEngNameInq)
        Me.gpbInq.Controls.Add(Me.rdbEngNameInq)
        Me.gpbInq.Controls.Add(Me.dtpRpdateStop)
        Me.gpbInq.Controls.Add(Me.Label15)
        Me.gpbInq.Controls.Add(Me.Label16)
        Me.gpbInq.Controls.Add(Me.dtpRpdateStart)
        Me.gpbInq.Controls.Add(Me.rdbRpdateInq)
        Me.gpbInq.Controls.Add(Me.rdbEngnoInq)
        Me.gpbInq.Controls.Add(Me.txtEngnoStop)
        Me.gpbInq.Controls.Add(Me.txtEngnoStart)
        Me.gpbInq.Controls.Add(Me.btnEnd)
        Me.gpbInq.Controls.Add(Me.btnInq)
        Me.gpbInq.Location = New System.Drawing.Point(82, 3)
        Me.gpbInq.Name = "gpbInq"
        Me.gpbInq.Size = New System.Drawing.Size(768, 88)
        Me.gpbInq.TabIndex = 13
        Me.gpbInq.TabStop = False
        Me.gpbInq.Text = "查詢條件"
        '
        'txtCopInq
        '
        Me.txtCopInq.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCopInq.Location = New System.Drawing.Point(528, 56)
        Me.txtCopInq.MaxLength = 80
        Me.txtCopInq.Name = "txtCopInq"
        Me.txtCopInq.Size = New System.Drawing.Size(112, 27)
        Me.txtCopInq.TabIndex = 88
        '
        'rdbCopInq
        '
        Me.rdbCopInq.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdbCopInq.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbCopInq.Location = New System.Drawing.Point(432, 56)
        Me.rdbCopInq.Name = "rdbCopInq"
        Me.rdbCopInq.Size = New System.Drawing.Size(96, 24)
        Me.rdbCopInq.TabIndex = 87
        Me.rdbCopInq.Text = "商號名稱"
        '
        'txtEngNameInq
        '
        Me.txtEngNameInq.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtEngNameInq.Location = New System.Drawing.Point(528, 24)
        Me.txtEngNameInq.MaxLength = 80
        Me.txtEngNameInq.Name = "txtEngNameInq"
        Me.txtEngNameInq.Size = New System.Drawing.Size(112, 27)
        Me.txtEngNameInq.TabIndex = 86
        '
        'rdbEngNameInq
        '
        Me.rdbEngNameInq.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbEngNameInq.Location = New System.Drawing.Point(432, 24)
        Me.rdbEngNameInq.Name = "rdbEngNameInq"
        Me.rdbEngNameInq.Size = New System.Drawing.Size(96, 24)
        Me.rdbEngNameInq.TabIndex = 61
        Me.rdbEngNameInq.Text = "工程名稱"
        '
        'dtpRpdateStop
        '
        Me.dtpRpdateStop.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpRpdateStop.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpRpdateStop.Location = New System.Drawing.Point(304, 56)
        Me.dtpRpdateStop.Name = "dtpRpdateStop"
        Me.dtpRpdateStop.Size = New System.Drawing.Size(112, 27)
        Me.dtpRpdateStop.TabIndex = 60
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.Location = New System.Drawing.Point(272, 64)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(32, 16)
        Me.Label15.TabIndex = 59
        Me.Label15.Text = "至"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label16.Location = New System.Drawing.Point(232, 32)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(32, 16)
        Me.Label16.TabIndex = 58
        Me.Label16.Text = "至"
        '
        'dtpRpdateStart
        '
        Me.dtpRpdateStart.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpRpdateStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpRpdateStart.Location = New System.Drawing.Point(152, 56)
        Me.dtpRpdateStart.Name = "dtpRpdateStart"
        Me.dtpRpdateStart.Size = New System.Drawing.Size(112, 27)
        Me.dtpRpdateStart.TabIndex = 57
        '
        'rdbRpdateInq
        '
        Me.rdbRpdateInq.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbRpdateInq.Location = New System.Drawing.Point(16, 56)
        Me.rdbRpdateInq.Name = "rdbRpdateInq"
        Me.rdbRpdateInq.Size = New System.Drawing.Size(128, 24)
        Me.rdbRpdateInq.TabIndex = 56
        Me.rdbRpdateInq.Text = "開標日期起訖"
        '
        'rdbEngnoInq
        '
        Me.rdbEngnoInq.Checked = True
        Me.rdbEngnoInq.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbEngnoInq.Location = New System.Drawing.Point(16, 24)
        Me.rdbEngnoInq.Name = "rdbEngnoInq"
        Me.rdbEngnoInq.Size = New System.Drawing.Size(128, 24)
        Me.rdbEngnoInq.TabIndex = 55
        Me.rdbEngnoInq.TabStop = True
        Me.rdbEngnoInq.Text = "工程代號起訖"
        '
        'txtEngnoStop
        '
        Me.txtEngnoStop.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtEngnoStop.Location = New System.Drawing.Point(272, 24)
        Me.txtEngnoStop.MaxLength = 7
        Me.txtEngnoStop.Name = "txtEngnoStop"
        Me.txtEngnoStop.Size = New System.Drawing.Size(64, 27)
        Me.txtEngnoStop.TabIndex = 54
        '
        'txtEngnoStart
        '
        Me.txtEngnoStart.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtEngnoStart.Location = New System.Drawing.Point(152, 24)
        Me.txtEngnoStart.MaxLength = 7
        Me.txtEngnoStart.Name = "txtEngnoStart"
        Me.txtEngnoStart.Size = New System.Drawing.Size(64, 27)
        Me.txtEngnoStart.TabIndex = 53
        '
        'btnEnd
        '
        Me.btnEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnEnd.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnEnd.Location = New System.Drawing.Point(680, 48)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(64, 32)
        Me.btnEnd.TabIndex = 45
        Me.btnEnd.Text = "結束"
        Me.btnEnd.UseVisualStyleBackColor = False
        '
        'btnInq
        '
        Me.btnInq.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnInq.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnInq.Location = New System.Drawing.Point(680, 16)
        Me.btnInq.Name = "btnInq"
        Me.btnInq.Size = New System.Drawing.Size(64, 32)
        Me.btnInq.TabIndex = 42
        Me.btnInq.Text = "查詢"
        Me.btnInq.UseVisualStyleBackColor = False
        '
        'chkKeepDate
        '
        Me.chkKeepDate.Location = New System.Drawing.Point(128, 248)
        Me.chkKeepDate.Name = "chkKeepDate"
        Me.chkKeepDate.Size = New System.Drawing.Size(16, 24)
        Me.chkKeepDate.TabIndex = 93
        Me.chkKeepDate.Text = "CheckBox1"
        '
        'dtpKeepDate
        '
        Me.dtpKeepDate.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpKeepDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpKeepDate.Location = New System.Drawing.Point(152, 248)
        Me.dtpKeepDate.Name = "dtpKeepDate"
        Me.dtpKeepDate.Size = New System.Drawing.Size(120, 27)
        Me.dtpKeepDate.TabIndex = 91
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 256)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 24)
        Me.Label2.TabIndex = 92
        Me.Label2.Text = "保固期限"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkTest2Date
        '
        Me.chkTest2Date.Location = New System.Drawing.Point(128, 216)
        Me.chkTest2Date.Name = "chkTest2Date"
        Me.chkTest2Date.Size = New System.Drawing.Size(16, 24)
        Me.chkTest2Date.TabIndex = 90
        Me.chkTest2Date.Text = "CheckBox1"
        '
        'chkOpenDate
        '
        Me.chkOpenDate.Location = New System.Drawing.Point(128, 184)
        Me.chkOpenDate.Name = "chkOpenDate"
        Me.chkOpenDate.Size = New System.Drawing.Size(16, 24)
        Me.chkOpenDate.TabIndex = 89
        Me.chkOpenDate.Text = "CheckBox1"
        '
        'txtEngname
        '
        Me.txtEngname.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtEngname.Location = New System.Drawing.Point(120, 56)
        Me.txtEngname.MaxLength = 80
        Me.txtEngname.Name = "txtEngname"
        Me.txtEngname.Size = New System.Drawing.Size(352, 27)
        Me.txtEngname.TabIndex = 85
        '
        'txtCop
        '
        Me.txtCop.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCop.Location = New System.Drawing.Point(120, 152)
        Me.txtCop.MaxLength = 50
        Me.txtCop.Name = "txtCop"
        Me.txtCop.Size = New System.Drawing.Size(192, 27)
        Me.txtCop.TabIndex = 80
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.Location = New System.Drawing.Point(24, 152)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 24)
        Me.Label11.TabIndex = 79
        Me.Label11.Text = "商號名稱"
        '
        'txtId
        '
        Me.txtId.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtId.Location = New System.Drawing.Point(120, 280)
        Me.txtId.Name = "txtId"
        Me.txtId.ReadOnly = True
        Me.txtId.Size = New System.Drawing.Size(56, 27)
        Me.txtId.TabIndex = 57
        '
        'txtEyear
        '
        Me.txtEyear.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtEyear.Location = New System.Drawing.Point(120, 88)
        Me.txtEyear.MaxLength = 3
        Me.txtEyear.Name = "txtEyear"
        Me.txtEyear.Size = New System.Drawing.Size(48, 27)
        Me.txtEyear.TabIndex = 68
        '
        'dtpTest2Date
        '
        Me.dtpTest2Date.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpTest2Date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTest2Date.Location = New System.Drawing.Point(152, 216)
        Me.dtpTest2Date.Name = "dtpTest2Date"
        Me.dtpTest2Date.Size = New System.Drawing.Size(120, 27)
        Me.dtpTest2Date.TabIndex = 67
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 589)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(984, 22)
        Me.StatusBar1.TabIndex = 14
        Me.StatusBar1.Text = "StatusBar1"
        '
        'dtpOpenDate
        '
        Me.dtpOpenDate.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpOpenDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOpenDate.Location = New System.Drawing.Point(152, 184)
        Me.dtpOpenDate.Name = "dtpOpenDate"
        Me.dtpOpenDate.Size = New System.Drawing.Size(120, 27)
        Me.dtpOpenDate.TabIndex = 65
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(82, 99)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(768, 384)
        Me.TabControl1.TabIndex = 12
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.DataGrid1)
        Me.TabPage1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(760, 357)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "多筆瀏覽"
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(24, 15)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ReadOnly = True
        Me.DataGrid1.Size = New System.Drawing.Size(712, 328)
        Me.DataGrid1.TabIndex = 2
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGrid1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "enf010"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "工程代號"
        Me.DataGridTextBoxColumn1.MappingName = "ENGNO"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "工程名稱"
        Me.DataGridTextBoxColumn2.MappingName = "ENGNAME"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 150
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "年度"
        Me.DataGridTextBoxColumn3.MappingName = "EYEAR"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 60
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "商號統編"
        Me.DataGridTextBoxColumn4.MappingName = "IDNO"
        Me.DataGridTextBoxColumn4.NullText = ""
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "商號名稱"
        Me.DataGridTextBoxColumn5.MappingName = "COP"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = "yyy/MM/dd"
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "開標日期"
        Me.DataGridTextBoxColumn6.MappingName = "OPEN_DATE"
        Me.DataGridTextBoxColumn6.NullText = ""
        Me.DataGridTextBoxColumn6.Width = 75
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = "yyy/MM/dd"
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "驗收日期"
        Me.DataGridTextBoxColumn7.MappingName = "TEST2_DATE"
        Me.DataGridTextBoxColumn7.NullText = ""
        Me.DataGridTextBoxColumn7.Width = 75
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = "yyy/MM/dd"
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "保固期限"
        Me.DataGridTextBoxColumn8.MappingName = "KEEP_DATE"
        Me.DataGridTextBoxColumn8.NullText = ""
        Me.DataGridTextBoxColumn8.Width = 75
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "自動編號"
        Me.DataGridTextBoxColumn9.MappingName = "AUTONO"
        Me.DataGridTextBoxColumn9.NullText = ""
        Me.DataGridTextBoxColumn9.Width = 75
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.chkKeepDate)
        Me.TabPage2.Controls.Add(Me.dtpKeepDate)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.chkTest2Date)
        Me.TabPage2.Controls.Add(Me.chkOpenDate)
        Me.TabPage2.Controls.Add(Me.txtEngname)
        Me.TabPage2.Controls.Add(Me.txtCop)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.txtId)
        Me.TabPage2.Controls.Add(Me.txtEyear)
        Me.TabPage2.Controls.Add(Me.dtpTest2Date)
        Me.TabPage2.Controls.Add(Me.dtpOpenDate)
        Me.TabPage2.Controls.Add(Me.txtIdno)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.txtEngno)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(760, 357)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "單筆編輯"
        Me.TabPage2.Visible = False
        '
        'txtIdno
        '
        Me.txtIdno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtIdno.Location = New System.Drawing.Point(120, 120)
        Me.txtIdno.MaxLength = 10
        Me.txtIdno.Name = "txtIdno"
        Me.txtIdno.Size = New System.Drawing.Size(96, 27)
        Me.txtIdno.TabIndex = 63
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 216)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 24)
        Me.Label9.TabIndex = 75
        Me.Label9.Text = "驗收日期"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.Location = New System.Drawing.Point(24, 184)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 24)
        Me.Label8.TabIndex = 74
        Me.Label8.Text = "開標日期"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.Location = New System.Drawing.Point(24, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 24)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "商號統編"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.Location = New System.Drawing.Point(56, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 24)
        Me.Label5.TabIndex = 70
        Me.Label5.Text = "年度"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 24)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "工程名稱"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(32, 288)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 24)
        Me.Label3.TabIndex = 66
        Me.Label3.Text = "識別編號"
        '
        'txtEngno
        '
        Me.txtEngno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtEngno.Location = New System.Drawing.Point(120, 24)
        Me.txtEngno.MaxLength = 7
        Me.txtEngno.Name = "txtEngno"
        Me.txtEngno.Size = New System.Drawing.Size(64, 27)
        Me.txtEngno.TabIndex = 59
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 24)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "工程代號"
        '
        'RecMove1
        '
        Me.RecMove1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RecMove1.FilesPara = Nothing
        Me.RecMove1.GenInsFunc = "() values ()"
        Me.RecMove1.genupdfunc = ""
        Me.RecMove1.Location = New System.Drawing.Point(0, 505)
        Me.RecMove1.Name = "RecMove1"
        Me.RecMove1.Setds = Nothing
        Me.RecMove1.Setpos = 0
        Me.RecMove1.Size = New System.Drawing.Size(984, 84)
        Me.RecMove1.TabIndex = 15
        '
        'BAILF030
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.RecMove1)
        Me.Controls.Add(Me.gpbInq)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.TabControl1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "BAILF030"
        Me.gpbInq.ResumeLayout(False)
        Me.gpbInq.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gpbInq As System.Windows.Forms.GroupBox
    Friend WithEvents txtCopInq As System.Windows.Forms.TextBox
    Friend WithEvents rdbCopInq As System.Windows.Forms.RadioButton
    Friend WithEvents txtEngNameInq As System.Windows.Forms.TextBox
    Friend WithEvents rdbEngNameInq As System.Windows.Forms.RadioButton
    Friend WithEvents dtpRpdateStop As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents dtpRpdateStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdbRpdateInq As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEngnoInq As System.Windows.Forms.RadioButton
    Friend WithEvents txtEngnoStop As System.Windows.Forms.TextBox
    Friend WithEvents txtEngnoStart As System.Windows.Forms.TextBox
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnInq As System.Windows.Forms.Button
    Friend WithEvents chkKeepDate As System.Windows.Forms.CheckBox
    Friend WithEvents dtpKeepDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkTest2Date As System.Windows.Forms.CheckBox
    Friend WithEvents chkOpenDate As System.Windows.Forms.CheckBox
    Friend WithEvents txtEngname As System.Windows.Forms.TextBox
    Friend WithEvents txtCop As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents txtEyear As System.Windows.Forms.TextBox
    Friend WithEvents dtpTest2Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents dtpOpenDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtIdno As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEngno As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RecMove1 As LAEACC_AP.RecMove

End Class
