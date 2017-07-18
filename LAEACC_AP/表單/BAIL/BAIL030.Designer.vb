<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BAIL030
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
        Me.chkPeriod = New System.Windows.Forms.CheckBox()
        Me.chk_dtpDateE = New System.Windows.Forms.CheckBox()
        Me.chk_dtpDateS = New System.Windows.Forms.CheckBox()
        Me.lblAmt = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.btnGetIdCop = New System.Windows.Forms.Button()
        Me.dtpEngKind3crE = New System.Windows.Forms.DateTimePicker()
        Me.lblEngKind3crE = New System.Windows.Forms.Label()
        Me.dtpEngKind3crS = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.dtg1 = New System.Windows.Forms.DataGrid()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.lblEngKind3crS = New System.Windows.Forms.Label()
        Me.txtEngIdno = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtEngCop = New System.Windows.Forms.TextBox()
        Me.dtpKind3crE = New System.Windows.Forms.DateTimePicker()
        Me.lblKind3crE = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbKind4 = New System.Windows.Forms.RadioButton()
        Me.rdbKind2 = New System.Windows.Forms.RadioButton()
        Me.rdbKind3 = New System.Windows.Forms.RadioButton()
        Me.rdbKind1 = New System.Windows.Forms.RadioButton()
        Me.dtpkind3crS = New System.Windows.Forms.DateTimePicker()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.lblKind3crS = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblEngname = New System.Windows.Forms.Label()
        Me.txtAmtr = New System.Windows.Forms.TextBox()
        Me.txtChknor = New System.Windows.Forms.TextBox()
        Me.txtBankr = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtIdno = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtCop = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtEngno = New System.Windows.Forms.TextBox()
        Me.btnInq = New System.Windows.Forms.Button()
        Me.txtInqEngno = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.dtg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkPeriod
        '
        Me.chkPeriod.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkPeriod.Location = New System.Drawing.Point(504, 400)
        Me.chkPeriod.Name = "chkPeriod"
        Me.chkPeriod.Size = New System.Drawing.Size(104, 24)
        Me.chkPeriod.TabIndex = 110
        Me.chkPeriod.Text = "分4期"
        '
        'chk_dtpDateE
        '
        Me.chk_dtpDateE.Location = New System.Drawing.Point(304, 368)
        Me.chk_dtpDateE.Name = "chk_dtpDateE"
        Me.chk_dtpDateE.Size = New System.Drawing.Size(16, 24)
        Me.chk_dtpDateE.TabIndex = 109
        Me.chk_dtpDateE.Text = "CheckBox1"
        '
        'chk_dtpDateS
        '
        Me.chk_dtpDateS.Location = New System.Drawing.Point(128, 368)
        Me.chk_dtpDateS.Name = "chk_dtpDateS"
        Me.chk_dtpDateS.Size = New System.Drawing.Size(16, 24)
        Me.chk_dtpDateS.TabIndex = 108
        Me.chk_dtpDateS.Text = "CheckBox1"
        '
        'lblAmt
        '
        Me.lblAmt.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblAmt.Location = New System.Drawing.Point(552, 360)
        Me.lblAmt.Name = "lblAmt"
        Me.lblAmt.Size = New System.Drawing.Size(96, 27)
        Me.lblAmt.TabIndex = 107
        Me.lblAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusBar1
        '
        Me.StatusBar1.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.StatusBar1.Location = New System.Drawing.Point(0, 441)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(760, 22)
        Me.StatusBar1.TabIndex = 106
        '
        'btnGetIdCop
        '
        Me.btnGetIdCop.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnGetIdCop.Location = New System.Drawing.Point(648, 256)
        Me.btnGetIdCop.Name = "btnGetIdCop"
        Me.btnGetIdCop.Size = New System.Drawing.Size(88, 32)
        Me.btnGetIdCop.TabIndex = 105
        Me.btnGetIdCop.Text = "取得資料"
        Me.btnGetIdCop.Visible = False
        '
        'dtpEngKind3crE
        '
        Me.dtpEngKind3crE.Enabled = False
        Me.dtpEngKind3crE.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpEngKind3crE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEngKind3crE.Location = New System.Drawing.Point(600, 40)
        Me.dtpEngKind3crE.Name = "dtpEngKind3crE"
        Me.dtpEngKind3crE.Size = New System.Drawing.Size(120, 27)
        Me.dtpEngKind3crE.TabIndex = 104
        '
        'lblEngKind3crE
        '
        Me.lblEngKind3crE.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblEngKind3crE.Location = New System.Drawing.Point(568, 40)
        Me.lblEngKind3crE.Name = "lblEngKind3crE"
        Me.lblEngKind3crE.Size = New System.Drawing.Size(24, 24)
        Me.lblEngKind3crE.TabIndex = 103
        Me.lblEngKind3crE.Text = "至"
        '
        'dtpEngKind3crS
        '
        Me.dtpEngKind3crS.Enabled = False
        Me.dtpEngKind3crS.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpEngKind3crS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEngKind3crS.Location = New System.Drawing.Point(440, 40)
        Me.dtpEngKind3crS.Name = "dtpEngKind3crS"
        Me.dtpEngKind3crS.Size = New System.Drawing.Size(120, 27)
        Me.dtpEngKind3crS.TabIndex = 102
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.Location = New System.Drawing.Point(192, 40)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(72, 24)
        Me.Label15.TabIndex = 97
        Me.Label15.Text = "商號名稱"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "種類"
        Me.DataGridTextBoxColumn1.MappingName = "ckind"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 40
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.dtg1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "bailf020"
        Me.DataGridTableStyle1.PreferredColumnWidth = 80
        '
        'dtg1
        '
        Me.dtg1.DataMember = ""
        Me.dtg1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtg1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dtg1.Location = New System.Drawing.Point(8, 72)
        Me.dtg1.Name = "dtg1"
        Me.dtg1.ReadOnly = True
        Me.dtg1.Size = New System.Drawing.Size(744, 176)
        Me.dtg1.TabIndex = 0
        Me.dtg1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = "yyy/MM/dd"
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "收入日期"
        Me.DataGridTextBoxColumn2.MappingName = "rpdate"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 90
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "支票號碼"
        Me.DataGridTextBoxColumn3.MappingName = "chkno"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 95
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "行庫"
        Me.DataGridTextBoxColumn4.MappingName = "bank"
        Me.DataGridTextBoxColumn4.NullText = ""
        Me.DataGridTextBoxColumn4.Width = 80
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn5.Format = "##,##0"
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "金額"
        Me.DataGridTextBoxColumn5.MappingName = "amt"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 110
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = "yyy/MM/dd"
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "保固期限起"
        Me.DataGridTextBoxColumn6.MappingName = "date_s"
        Me.DataGridTextBoxColumn6.NullText = ""
        Me.DataGridTextBoxColumn6.Width = 90
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = "yyy/MM/dd"
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "保固期限訖"
        Me.DataGridTextBoxColumn7.MappingName = "date_e"
        Me.DataGridTextBoxColumn7.NullText = ""
        Me.DataGridTextBoxColumn7.Width = 90
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = "yyy/MM/dd"
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "退還日期"
        Me.DataGridTextBoxColumn8.MappingName = "pdate"
        Me.DataGridTextBoxColumn8.NullText = ""
        Me.DataGridTextBoxColumn8.Width = 90
        '
        'lblEngKind3crS
        '
        Me.lblEngKind3crS.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblEngKind3crS.Location = New System.Drawing.Point(376, 40)
        Me.lblEngKind3crS.Name = "lblEngKind3crS"
        Me.lblEngKind3crS.Size = New System.Drawing.Size(56, 24)
        Me.lblEngKind3crS.TabIndex = 101
        Me.lblEngKind3crS.Text = "保固："
        Me.lblEngKind3crS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEngIdno
        '
        Me.txtEngIdno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtEngIdno.Location = New System.Drawing.Point(104, 40)
        Me.txtEngIdno.Name = "txtEngIdno"
        Me.txtEngIdno.ReadOnly = True
        Me.txtEngIdno.Size = New System.Drawing.Size(88, 27)
        Me.txtEngIdno.TabIndex = 99
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 40)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 24)
        Me.Label12.TabIndex = 100
        Me.Label12.Text = "商號統編"
        '
        'txtEngCop
        '
        Me.txtEngCop.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtEngCop.Location = New System.Drawing.Point(272, 40)
        Me.txtEngCop.MaxLength = 8
        Me.txtEngCop.Name = "txtEngCop"
        Me.txtEngCop.ReadOnly = True
        Me.txtEngCop.Size = New System.Drawing.Size(96, 27)
        Me.txtEngCop.TabIndex = 98
        '
        'dtpKind3crE
        '
        Me.dtpKind3crE.Enabled = False
        Me.dtpKind3crE.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpKind3crE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpKind3crE.Location = New System.Drawing.Point(320, 368)
        Me.dtpKind3crE.Name = "dtpKind3crE"
        Me.dtpKind3crE.Size = New System.Drawing.Size(120, 27)
        Me.dtpKind3crE.TabIndex = 95
        '
        'lblKind3crE
        '
        Me.lblKind3crE.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblKind3crE.Location = New System.Drawing.Point(272, 368)
        Me.lblKind3crE.Name = "lblKind3crE"
        Me.lblKind3crE.Size = New System.Drawing.Size(24, 24)
        Me.lblKind3crE.TabIndex = 94
        Me.lblKind3crE.Text = "至"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbKind4)
        Me.GroupBox1.Controls.Add(Me.rdbKind2)
        Me.GroupBox1.Controls.Add(Me.rdbKind3)
        Me.GroupBox1.Controls.Add(Me.rdbKind1)
        Me.GroupBox1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 288)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(480, 72)
        Me.GroupBox1.TabIndex = 96
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "保證種類"
        '
        'rdbKind4
        '
        Me.rdbKind4.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbKind4.Location = New System.Drawing.Point(336, 32)
        Me.rdbKind4.Name = "rdbKind4"
        Me.rdbKind4.Size = New System.Drawing.Size(136, 24)
        Me.rdbKind4.TabIndex = 4
        Me.rdbKind4.Text = "4.收差額保證品"
        '
        'rdbKind2
        '
        Me.rdbKind2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbKind2.Location = New System.Drawing.Point(112, 32)
        Me.rdbKind2.Name = "rdbKind2"
        Me.rdbKind2.Size = New System.Drawing.Size(112, 24)
        Me.rdbKind2.TabIndex = 3
        Me.rdbKind2.Text = "2.收押標品"
        '
        'rdbKind3
        '
        Me.rdbKind3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbKind3.Location = New System.Drawing.Point(232, 32)
        Me.rdbKind3.Name = "rdbKind3"
        Me.rdbKind3.Size = New System.Drawing.Size(104, 24)
        Me.rdbKind3.TabIndex = 2
        Me.rdbKind3.Text = "3.收保固品"
        '
        'rdbKind1
        '
        Me.rdbKind1.Checked = True
        Me.rdbKind1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbKind1.Location = New System.Drawing.Point(8, 32)
        Me.rdbKind1.Name = "rdbKind1"
        Me.rdbKind1.Size = New System.Drawing.Size(104, 24)
        Me.rdbKind1.TabIndex = 0
        Me.rdbKind1.TabStop = True
        Me.rdbKind1.Text = "1.收履約品"
        '
        'dtpkind3crS
        '
        Me.dtpkind3crS.Enabled = False
        Me.dtpkind3crS.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpkind3crS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpkind3crS.Location = New System.Drawing.Point(144, 368)
        Me.dtpkind3crS.Name = "dtpkind3crS"
        Me.dtpkind3crS.Size = New System.Drawing.Size(120, 27)
        Me.dtpkind3crS.TabIndex = 93
        '
        'btnEnd
        '
        Me.btnEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnEnd.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnEnd.Location = New System.Drawing.Point(813, 2)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(64, 32)
        Me.btnEnd.TabIndex = 48
        Me.btnEnd.Text = "結束"
        Me.btnEnd.UseVisualStyleBackColor = False
        '
        'lblKind3crS
        '
        Me.lblKind3crS.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblKind3crS.Location = New System.Drawing.Point(16, 368)
        Me.lblKind3crS.Name = "lblKind3crS"
        Me.lblKind3crS.Size = New System.Drawing.Size(112, 24)
        Me.lblKind3crS.TabIndex = 92
        Me.lblKind3crS.Text = "保管品期限："
        Me.lblKind3crS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(125, 42)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(768, 496)
        Me.TabControl1.TabIndex = 47
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.chkPeriod)
        Me.TabPage1.Controls.Add(Me.chk_dtpDateE)
        Me.TabPage1.Controls.Add(Me.chk_dtpDateS)
        Me.TabPage1.Controls.Add(Me.lblAmt)
        Me.TabPage1.Controls.Add(Me.StatusBar1)
        Me.TabPage1.Controls.Add(Me.btnGetIdCop)
        Me.TabPage1.Controls.Add(Me.dtpEngKind3crE)
        Me.TabPage1.Controls.Add(Me.lblEngKind3crE)
        Me.TabPage1.Controls.Add(Me.dtpEngKind3crS)
        Me.TabPage1.Controls.Add(Me.lblEngKind3crS)
        Me.TabPage1.Controls.Add(Me.txtEngIdno)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.txtEngCop)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.dtpKind3crE)
        Me.TabPage1.Controls.Add(Me.lblKind3crE)
        Me.TabPage1.Controls.Add(Me.dtpkind3crS)
        Me.TabPage1.Controls.Add(Me.lblKind3crS)
        Me.TabPage1.Controls.Add(Me.btnCancel)
        Me.TabPage1.Controls.Add(Me.btnAdd)
        Me.TabPage1.Controls.Add(Me.lblEngname)
        Me.TabPage1.Controls.Add(Me.txtAmtr)
        Me.TabPage1.Controls.Add(Me.txtChknor)
        Me.TabPage1.Controls.Add(Me.txtBankr)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.dtpDate)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.txtIdno)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.txtCop)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtEngno)
        Me.TabPage1.Controls.Add(Me.dtg1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(760, 463)
        Me.TabPage1.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Aqua
        Me.btnCancel.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(384, 400)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 32)
        Me.btnCancel.TabIndex = 91
        Me.btnCancel.Text = "取消"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.Aqua
        Me.btnAdd.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(304, 400)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(64, 32)
        Me.btnAdd.TabIndex = 90
        Me.btnAdd.Text = "新增"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'lblEngname
        '
        Me.lblEngname.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblEngname.Location = New System.Drawing.Point(176, 8)
        Me.lblEngname.Name = "lblEngname"
        Me.lblEngname.Size = New System.Drawing.Size(568, 24)
        Me.lblEngname.TabIndex = 89
        '
        'txtAmtr
        '
        Me.txtAmtr.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtAmtr.Location = New System.Drawing.Point(656, 360)
        Me.txtAmtr.MaxLength = 10
        Me.txtAmtr.Name = "txtAmtr"
        Me.txtAmtr.Size = New System.Drawing.Size(96, 27)
        Me.txtAmtr.TabIndex = 88
        '
        'txtChknor
        '
        Me.txtChknor.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtChknor.Location = New System.Drawing.Point(552, 328)
        Me.txtChknor.MaxLength = 12
        Me.txtChknor.Name = "txtChknor"
        Me.txtChknor.Size = New System.Drawing.Size(96, 27)
        Me.txtChknor.TabIndex = 87
        '
        'txtBankr
        '
        Me.txtBankr.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtBankr.Location = New System.Drawing.Point(552, 288)
        Me.txtBankr.MaxLength = 20
        Me.txtBankr.Name = "txtBankr"
        Me.txtBankr.Size = New System.Drawing.Size(96, 27)
        Me.txtBankr.TabIndex = 86
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label19.Location = New System.Drawing.Point(496, 360)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 24)
        Me.Label19.TabIndex = 76
        Me.Label19.Text = "金額="
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label20.Location = New System.Drawing.Point(496, 328)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(48, 24)
        Me.Label20.TabIndex = 75
        Me.Label20.Text = "支票="
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label21.Location = New System.Drawing.Point(496, 296)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(48, 24)
        Me.Label21.TabIndex = 74
        Me.Label21.Text = "行庫="
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpDate
        '
        Me.dtpDate.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(512, 256)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(120, 27)
        Me.dtpDate.TabIndex = 60
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label13.Location = New System.Drawing.Point(424, 256)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 24)
        Me.Label13.TabIndex = 59
        Me.Label13.Text = "收款日期"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtIdno
        '
        Me.txtIdno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtIdno.Location = New System.Drawing.Point(88, 256)
        Me.txtIdno.MaxLength = 10
        Me.txtIdno.Name = "txtIdno"
        Me.txtIdno.Size = New System.Drawing.Size(96, 27)
        Me.txtIdno.TabIndex = 57
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label18.Location = New System.Drawing.Point(8, 256)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(72, 24)
        Me.Label18.TabIndex = 58
        Me.Label18.Text = "商號統編"
        '
        'txtCop
        '
        Me.txtCop.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCop.Location = New System.Drawing.Point(280, 256)
        Me.txtCop.MaxLength = 50
        Me.txtCop.Name = "txtCop"
        Me.txtCop.Size = New System.Drawing.Size(120, 27)
        Me.txtCop.TabIndex = 56
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.Location = New System.Drawing.Point(192, 256)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 24)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "商號名稱"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 23)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "工程代號"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEngno
        '
        Me.txtEngno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtEngno.Location = New System.Drawing.Point(104, 8)
        Me.txtEngno.Name = "txtEngno"
        Me.txtEngno.ReadOnly = True
        Me.txtEngno.Size = New System.Drawing.Size(64, 27)
        Me.txtEngno.TabIndex = 21
        '
        'btnInq
        '
        Me.btnInq.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnInq.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnInq.Location = New System.Drawing.Point(341, 2)
        Me.btnInq.Name = "btnInq"
        Me.btnInq.Size = New System.Drawing.Size(64, 32)
        Me.btnInq.TabIndex = 46
        Me.btnInq.Text = "查詢"
        Me.btnInq.UseVisualStyleBackColor = False
        '
        'txtInqEngno
        '
        Me.txtInqEngno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtInqEngno.Location = New System.Drawing.Point(229, 2)
        Me.txtInqEngno.MaxLength = 7
        Me.txtInqEngno.Name = "txtInqEngno"
        Me.txtInqEngno.Size = New System.Drawing.Size(64, 27)
        Me.txtInqEngno.TabIndex = 45
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(133, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 23)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "工程代號"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BAIL030
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnInq)
        Me.Controls.Add(Me.txtInqEngno)
        Me.Controls.Add(Me.Label2)
        Me.Name = "BAIL030"
        CType(Me.dtg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkPeriod As System.Windows.Forms.CheckBox
    Friend WithEvents chk_dtpDateE As System.Windows.Forms.CheckBox
    Friend WithEvents chk_dtpDateS As System.Windows.Forms.CheckBox
    Friend WithEvents lblAmt As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents btnGetIdCop As System.Windows.Forms.Button
    Friend WithEvents dtpEngKind3crE As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEngKind3crE As System.Windows.Forms.Label
    Friend WithEvents dtpEngKind3crS As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents dtg1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblEngKind3crS As System.Windows.Forms.Label
    Friend WithEvents txtEngIdno As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtEngCop As System.Windows.Forms.TextBox
    Friend WithEvents dtpKind3crE As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblKind3crE As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbKind4 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbKind2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbKind3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbKind1 As System.Windows.Forms.RadioButton
    Friend WithEvents dtpkind3crS As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents lblKind3crS As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblEngname As System.Windows.Forms.Label
    Friend WithEvents txtAmtr As System.Windows.Forms.TextBox
    Friend WithEvents txtChknor As System.Windows.Forms.TextBox
    Friend WithEvents txtBankr As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtIdno As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtCop As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEngno As System.Windows.Forms.TextBox
    Friend WithEvents btnInq As System.Windows.Forms.Button
    Friend WithEvents txtInqEngno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
