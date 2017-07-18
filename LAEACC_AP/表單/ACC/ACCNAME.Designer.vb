<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ACCNAME
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
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnSearch = New System.Windows.Forms.Button()
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
        Me.vxtBook = New System.Windows.Forms.MaskedTextBox()
        Me.vxtAccno = New System.Windows.Forms.MaskedTextBox()
        Me.nudOutYear = New System.Windows.Forms.NumericUpDown()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblAccno = New System.Windows.Forms.Label()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBelong = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAccount_no = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtStaff_no = New System.Windows.Forms.TextBox()
        Me.txtBank = New System.Windows.Forms.TextBox()
        Me.txtAccname = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.ChkBelong = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtQunit = New System.Windows.Forms.TextBox()
        Me.txtQaccount_no = New System.Windows.Forms.TextBox()
        Me.txtQstaff_no = New System.Windows.Forms.TextBox()
        Me.txtQaccname = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblError = New System.Windows.Forms.Label()
        Me.cboAccno = New System.Windows.Forms.ComboBox()
        Me.lblFinish = New System.Windows.Forms.Label()
        Me.btnBook = New System.Windows.Forms.Button()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rdoOutyearNo = New System.Windows.Forms.RadioButton()
        Me.rdoOutyearYes = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoExcel = New System.Windows.Forms.RadioButton()
        Me.rdoPreview = New System.Windows.Forms.RadioButton()
        Me.rdoPrint = New System.Windows.Forms.RadioButton()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.vxtAccnoEnd = New System.Windows.Forms.MaskedTextBox()
        Me.vxtAccnoStart = New System.Windows.Forms.MaskedTextBox()
        Me.lblMsgOutyear = New System.Windows.Forms.Label()
        Me.btnOutyear = New System.Windows.Forms.Button()
        Me.nudOutYearT = New System.Windows.Forms.NumericUpDown()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RecMove1 = New LAEACC_AP.RecMove()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.nudOutYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        CType(Me.nudOutYearT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'vxtEndNo
        '
        Me.vxtEndNo.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtEndNo.Location = New System.Drawing.Point(386, 4)
        Me.vxtEndNo.Mask = "#-####-##-##-#######-#"
        Me.vxtEndNo.Name = "vxtEndNo"
        Me.vxtEndNo.Size = New System.Drawing.Size(168, 27)
        Me.vxtEndNo.TabIndex = 125
        '
        'vxtStartNo
        '
        Me.vxtStartNo.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtStartNo.Location = New System.Drawing.Point(133, 2)
        Me.vxtStartNo.Mask = "#-####-##-##-#######-#"
        Me.vxtStartNo.Name = "vxtStartNo"
        Me.vxtStartNo.Size = New System.Drawing.Size(185, 27)
        Me.vxtStartNo.TabIndex = 124
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.Location = New System.Drawing.Point(624, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(56, 32)
        Me.btnExit.TabIndex = 27
        Me.btnExit.Text = "離開"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.Location = New System.Drawing.Point(344, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 16)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "訖值"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BtnSearch
        '
        Me.BtnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnSearch.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(560, 4)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(56, 32)
        Me.BtnSearch.TabIndex = 26
        Me.BtnSearch.Text = "查詢"
        Me.BtnSearch.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(8, 44)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(736, 432)
        Me.TabControl1.TabIndex = 23
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGrid1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(728, 402)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "多筆瀏覽"
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(0, 8)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(728, 392)
        Me.DataGrid1.TabIndex = 1
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGrid1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "accname"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "會計科目"
        Me.DataGridTextBoxColumn1.MappingName = "accno"
        Me.DataGridTextBoxColumn1.Width = 150
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "科目名稱"
        Me.DataGridTextBoxColumn2.MappingName = "accname"
        Me.DataGridTextBoxColumn2.Width = 300
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "歸屬系統"
        Me.DataGridTextBoxColumn3.MappingName = "belong"
        Me.DataGridTextBoxColumn3.Width = 75
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "往來銀行"
        Me.DataGridTextBoxColumn4.MappingName = "bank"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "預算單位"
        Me.DataGridTextBoxColumn5.MappingName = "unit"
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "預算控制者"
        Me.DataGridTextBoxColumn6.MappingName = "staff_no"
        Me.DataGridTextBoxColumn6.Width = 75
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "主計審核者"
        Me.DataGridTextBoxColumn7.MappingName = "account_no"
        Me.DataGridTextBoxColumn7.Width = 75
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "計帳科目"
        Me.DataGridTextBoxColumn8.MappingName = "bookaccno"
        Me.DataGridTextBoxColumn8.Width = 140
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "結轉年度"
        Me.DataGridTextBoxColumn9.MappingName = "outyear"
        Me.DataGridTextBoxColumn9.Width = 75
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.vxtBook)
        Me.TabPage2.Controls.Add(Me.vxtAccno)
        Me.TabPage2.Controls.Add(Me.nudOutYear)
        Me.TabPage2.Controls.Add(Me.Label21)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.lblAccno)
        Me.TabPage2.Controls.Add(Me.txtUnit)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.txtBelong)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.txtAccount_no)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.txtStaff_no)
        Me.TabPage2.Controls.Add(Me.txtBank)
        Me.TabPage2.Controls.Add(Me.txtAccname)
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(728, 402)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "單筆明細"
        '
        'vxtBook
        '
        Me.vxtBook.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtBook.Location = New System.Drawing.Point(160, 320)
        Me.vxtBook.Mask = "#-####-##-##-#######-#"
        Me.vxtBook.Name = "vxtBook"
        Me.vxtBook.Size = New System.Drawing.Size(185, 27)
        Me.vxtBook.TabIndex = 126
        '
        'vxtAccno
        '
        Me.vxtAccno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccno.Location = New System.Drawing.Point(160, 40)
        Me.vxtAccno.Mask = "#-####-##-##-#######-#"
        Me.vxtAccno.Name = "vxtAccno"
        Me.vxtAccno.Size = New System.Drawing.Size(185, 27)
        Me.vxtAccno.TabIndex = 125
        '
        'nudOutYear
        '
        Me.nudOutYear.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.nudOutYear.Location = New System.Drawing.Point(160, 360)
        Me.nudOutYear.Maximum = New Decimal(New Integer() {199, 0, 0, 0})
        Me.nudOutYear.Name = "nudOutYear"
        Me.nudOutYear.Size = New System.Drawing.Size(72, 27)
        Me.nudOutYear.TabIndex = 19
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(64, 360)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(80, 23)
        Me.Label21.TabIndex = 18
        Me.Label21.Text = "結轉年度"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(64, 320)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(80, 23)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "記帳科目"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAccno
        '
        Me.lblAccno.Location = New System.Drawing.Point(520, 24)
        Me.lblAccno.Name = "lblAccno"
        Me.lblAccno.Size = New System.Drawing.Size(112, 23)
        Me.lblAccno.TabIndex = 15
        Me.lblAccno.Text = "lblAccno"
        '
        'txtUnit
        '
        Me.txtUnit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtUnit.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtUnit.Location = New System.Drawing.Point(160, 200)
        Me.txtUnit.MaxLength = 5
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.Size = New System.Drawing.Size(56, 27)
        Me.txtUnit.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(72, 200)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(80, 23)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "預算單位"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBelong
        '
        Me.txtBelong.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBelong.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtBelong.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtBelong.Location = New System.Drawing.Point(160, 120)
        Me.txtBelong.MaxLength = 1
        Me.txtBelong.Name = "txtBelong"
        Me.txtBelong.Size = New System.Drawing.Size(32, 27)
        Me.txtBelong.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(72, 120)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(80, 23)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "歸屬單位"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAccount_no
        '
        Me.txtAccount_no.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtAccount_no.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtAccount_no.Location = New System.Drawing.Point(160, 280)
        Me.txtAccount_no.MaxLength = 4
        Me.txtAccount_no.Name = "txtAccount_no"
        Me.txtAccount_no.Size = New System.Drawing.Size(56, 27)
        Me.txtAccount_no.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(8, 280)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(136, 23)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "主計預算審核者"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(72, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(80, 24)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "科目代號"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(16, 240)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(136, 23)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "單位預算控制者"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(72, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(80, 23)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "往來銀行"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(72, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(80, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "科目名稱"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtStaff_no
        '
        Me.txtStaff_no.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtStaff_no.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtStaff_no.Location = New System.Drawing.Point(160, 240)
        Me.txtStaff_no.MaxLength = 4
        Me.txtStaff_no.Name = "txtStaff_no"
        Me.txtStaff_no.Size = New System.Drawing.Size(56, 27)
        Me.txtStaff_no.TabIndex = 8
        '
        'txtBank
        '
        Me.txtBank.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtBank.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtBank.Location = New System.Drawing.Point(160, 160)
        Me.txtBank.MaxLength = 2
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(40, 27)
        Me.txtBank.TabIndex = 6
        '
        'txtAccname
        '
        Me.txtAccname.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtAccname.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtAccname.Location = New System.Drawing.Point(160, 80)
        Me.txtAccname.MaxLength = 50
        Me.txtAccname.Name = "txtAccname"
        Me.txtAccname.Size = New System.Drawing.Size(416, 27)
        Me.txtAccname.TabIndex = 4
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.ChkBelong)
        Me.TabPage3.Controls.Add(Me.Label10)
        Me.TabPage3.Controls.Add(Me.txtQunit)
        Me.TabPage3.Controls.Add(Me.txtQaccount_no)
        Me.TabPage3.Controls.Add(Me.txtQstaff_no)
        Me.TabPage3.Controls.Add(Me.txtQaccname)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.Label12)
        Me.TabPage3.Controls.Add(Me.Label13)
        Me.TabPage3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabPage3.Location = New System.Drawing.Point(4, 26)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(728, 402)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "條件設定"
        '
        'ChkBelong
        '
        Me.ChkBelong.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ChkBelong.Location = New System.Drawing.Point(184, 256)
        Me.ChkBelong.Name = "ChkBelong"
        Me.ChkBelong.Size = New System.Drawing.Size(176, 24)
        Me.ChkBelong.TabIndex = 15
        Me.ChkBelong.Text = "過濾掉預算專屬科目(B)"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.Location = New System.Drawing.Point(16, 208)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(160, 23)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "主計預算審核者"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtQunit
        '
        Me.txtQunit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtQunit.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtQunit.Location = New System.Drawing.Point(184, 104)
        Me.txtQunit.MaxLength = 5
        Me.txtQunit.Name = "txtQunit"
        Me.txtQunit.Size = New System.Drawing.Size(56, 27)
        Me.txtQunit.TabIndex = 12
        '
        'txtQaccount_no
        '
        Me.txtQaccount_no.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtQaccount_no.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtQaccount_no.Location = New System.Drawing.Point(184, 208)
        Me.txtQaccount_no.MaxLength = 4
        Me.txtQaccount_no.Name = "txtQaccount_no"
        Me.txtQaccount_no.Size = New System.Drawing.Size(56, 27)
        Me.txtQaccount_no.TabIndex = 14
        '
        'txtQstaff_no
        '
        Me.txtQstaff_no.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtQstaff_no.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtQstaff_no.Location = New System.Drawing.Point(184, 152)
        Me.txtQstaff_no.MaxLength = 4
        Me.txtQstaff_no.Name = "txtQstaff_no"
        Me.txtQstaff_no.Size = New System.Drawing.Size(56, 27)
        Me.txtQstaff_no.TabIndex = 13
        '
        'txtQaccname
        '
        Me.txtQaccname.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtQaccname.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtQaccname.Location = New System.Drawing.Point(184, 56)
        Me.txtQaccname.MaxLength = 60
        Me.txtQaccname.Name = "txtQaccname"
        Me.txtQaccname.Size = New System.Drawing.Size(376, 27)
        Me.txtQaccname.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.Location = New System.Drawing.Point(56, 104)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 23)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "預算單位"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label12.Location = New System.Drawing.Point(16, 152)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(160, 23)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "單位預算控制者"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label13.Location = New System.Drawing.Point(56, 64)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(120, 24)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "科目名稱"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage4.Controls.Add(Me.Label15)
        Me.TabPage4.Controls.Add(Me.lblError)
        Me.TabPage4.Controls.Add(Me.cboAccno)
        Me.TabPage4.Controls.Add(Me.lblFinish)
        Me.TabPage4.Controls.Add(Me.btnBook)
        Me.TabPage4.Location = New System.Drawing.Point(4, 26)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(728, 402)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "更新記帳科目"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.Location = New System.Drawing.Point(32, 32)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(440, 40)
        Me.Label15.TabIndex = 18
        Me.Label15.Text = "本作業在將每一預算控制科目填上記帳科目"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblError
        '
        Me.lblError.Location = New System.Drawing.Point(352, 120)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(248, 192)
        Me.lblError.TabIndex = 3
        '
        'cboAccno
        '
        Me.cboAccno.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.cboAccno.Location = New System.Drawing.Point(152, 112)
        Me.cboAccno.Name = "cboAccno"
        Me.cboAccno.Size = New System.Drawing.Size(121, 24)
        Me.cboAccno.TabIndex = 16
        Me.cboAccno.Text = "cboAccno"
        Me.cboAccno.Visible = False
        '
        'lblFinish
        '
        Me.lblFinish.Location = New System.Drawing.Point(144, 272)
        Me.lblFinish.Name = "lblFinish"
        Me.lblFinish.Size = New System.Drawing.Size(184, 40)
        Me.lblFinish.TabIndex = 1
        '
        'btnBook
        '
        Me.btnBook.BackColor = System.Drawing.Color.Magenta
        Me.btnBook.Location = New System.Drawing.Point(160, 176)
        Me.btnBook.Name = "btnBook"
        Me.btnBook.Size = New System.Drawing.Size(136, 48)
        Me.btnBook.TabIndex = 17
        Me.btnBook.Text = "確定執行"
        Me.btnBook.UseVisualStyleBackColor = False
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.MediumAquamarine
        Me.TabPage5.Controls.Add(Me.GroupBox2)
        Me.TabPage5.Controls.Add(Me.GroupBox1)
        Me.TabPage5.Controls.Add(Me.Label16)
        Me.TabPage5.Controls.Add(Me.btnPrint)
        Me.TabPage5.Location = New System.Drawing.Point(4, 26)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(728, 402)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "列印"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.rdoOutyearNo)
        Me.GroupBox2.Controls.Add(Me.rdoOutyearYes)
        Me.GroupBox2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.GroupBox2.Location = New System.Drawing.Point(168, 96)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(176, 67)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "歷史科目列印否?"
        '
        'rdoOutyearNo
        '
        Me.rdoOutyearNo.Checked = True
        Me.rdoOutyearNo.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoOutyearNo.ForeColor = System.Drawing.Color.Yellow
        Me.rdoOutyearNo.Location = New System.Drawing.Point(104, 32)
        Me.rdoOutyearNo.Name = "rdoOutyearNo"
        Me.rdoOutyearNo.Size = New System.Drawing.Size(64, 24)
        Me.rdoOutyearNo.TabIndex = 1
        Me.rdoOutyearNo.TabStop = True
        Me.rdoOutyearNo.Text = "否"
        '
        'rdoOutyearYes
        '
        Me.rdoOutyearYes.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoOutyearYes.ForeColor = System.Drawing.Color.Yellow
        Me.rdoOutyearYes.Location = New System.Drawing.Point(32, 32)
        Me.rdoOutyearYes.Name = "rdoOutyearYes"
        Me.rdoOutyearYes.Size = New System.Drawing.Size(56, 24)
        Me.rdoOutyearYes.TabIndex = 0
        Me.rdoOutyearYes.Text = "是"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rdoExcel)
        Me.GroupBox1.Controls.Add(Me.rdoPreview)
        Me.GroupBox1.Controls.Add(Me.rdoPrint)
        Me.GroupBox1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.GroupBox1.Location = New System.Drawing.Point(168, 176)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(464, 67)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "列印方式"
        '
        'rdoExcel
        '
        Me.rdoExcel.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoExcel.ForeColor = System.Drawing.Color.Yellow
        Me.rdoExcel.Location = New System.Drawing.Point(304, 32)
        Me.rdoExcel.Name = "rdoExcel"
        Me.rdoExcel.Size = New System.Drawing.Size(144, 24)
        Me.rdoExcel.TabIndex = 2
        Me.rdoExcel.Text = "產生至excel"
        '
        'rdoPreview
        '
        Me.rdoPreview.Checked = True
        Me.rdoPreview.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPreview.ForeColor = System.Drawing.Color.Yellow
        Me.rdoPreview.Location = New System.Drawing.Point(168, 32)
        Me.rdoPreview.Name = "rdoPreview"
        Me.rdoPreview.Size = New System.Drawing.Size(120, 24)
        Me.rdoPreview.TabIndex = 1
        Me.rdoPreview.TabStop = True
        Me.rdoPreview.Text = "預覽列印"
        '
        'rdoPrint
        '
        Me.rdoPrint.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPrint.ForeColor = System.Drawing.Color.Yellow
        Me.rdoPrint.Location = New System.Drawing.Point(32, 32)
        Me.rdoPrint.Name = "rdoPrint"
        Me.rdoPrint.Size = New System.Drawing.Size(120, 24)
        Me.rdoPrint.TabIndex = 0
        Me.rdoPrint.Text = "直接列印"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(176, 56)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(392, 32)
        Me.Label16.TabIndex = 9
        Me.Label16.Text = "列印起訖科目範圍之會計科目"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.Yellow
        Me.btnPrint.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(232, 264)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(80, 32)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Text = "確定"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.Navy
        Me.TabPage6.Controls.Add(Me.vxtAccnoEnd)
        Me.TabPage6.Controls.Add(Me.vxtAccnoStart)
        Me.TabPage6.Controls.Add(Me.lblMsgOutyear)
        Me.TabPage6.Controls.Add(Me.btnOutyear)
        Me.TabPage6.Controls.Add(Me.nudOutYearT)
        Me.TabPage6.Controls.Add(Me.Label20)
        Me.TabPage6.Controls.Add(Me.Label19)
        Me.TabPage6.Controls.Add(Me.Label18)
        Me.TabPage6.Controls.Add(Me.Label17)
        Me.TabPage6.Location = New System.Drawing.Point(4, 26)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(728, 402)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "轉為歷史科目"
        '
        'vxtAccnoEnd
        '
        Me.vxtAccnoEnd.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccnoEnd.Location = New System.Drawing.Point(208, 113)
        Me.vxtAccnoEnd.Mask = "#-####-##-##-#######-#"
        Me.vxtAccnoEnd.Name = "vxtAccnoEnd"
        Me.vxtAccnoEnd.Size = New System.Drawing.Size(185, 27)
        Me.vxtAccnoEnd.TabIndex = 127
        '
        'vxtAccnoStart
        '
        Me.vxtAccnoStart.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccnoStart.Location = New System.Drawing.Point(208, 56)
        Me.vxtAccnoStart.Mask = "#-####-##-##-#######-#"
        Me.vxtAccnoStart.Name = "vxtAccnoStart"
        Me.vxtAccnoStart.Size = New System.Drawing.Size(185, 27)
        Me.vxtAccnoStart.TabIndex = 126
        '
        'lblMsgOutyear
        '
        Me.lblMsgOutyear.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblMsgOutyear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblMsgOutyear.Location = New System.Drawing.Point(72, 336)
        Me.lblMsgOutyear.Name = "lblMsgOutyear"
        Me.lblMsgOutyear.Size = New System.Drawing.Size(632, 48)
        Me.lblMsgOutyear.TabIndex = 23
        '
        'btnOutyear
        '
        Me.btnOutyear.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOutyear.Location = New System.Drawing.Point(208, 288)
        Me.btnOutyear.Name = "btnOutyear"
        Me.btnOutyear.Size = New System.Drawing.Size(80, 32)
        Me.btnOutyear.TabIndex = 22
        Me.btnOutyear.Text = "確定執行"
        Me.btnOutyear.UseVisualStyleBackColor = False
        '
        'nudOutYearT
        '
        Me.nudOutYearT.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nudOutYearT.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.nudOutYearT.Location = New System.Drawing.Point(208, 160)
        Me.nudOutYearT.Maximum = New Decimal(New Integer() {199, 0, 0, 0})
        Me.nudOutYearT.Name = "nudOutYearT"
        Me.nudOutYearT.Size = New System.Drawing.Size(72, 30)
        Me.nudOutYearT.TabIndex = 21
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.White
        Me.Label20.Location = New System.Drawing.Point(56, 160)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(152, 23)
        Me.Label20.TabIndex = 20
        Me.Label20.Text = "結轉年度"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(88, 232)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(560, 48)
        Me.Label19.TabIndex = 19
        Me.Label19.Text = "將起訖科目皆轉為歷史科目,     也就是在outyear欄填上結轉年度"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(48, 112)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(152, 23)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "訖值"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(48, 56)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(152, 23)
        Me.Label17.TabIndex = 5
        Me.Label17.Text = "請輸入科目起值"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 23)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "請輸入科目起值"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RecMove1
        '
        Me.RecMove1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RecMove1.FilesPara = Nothing
        Me.RecMove1.GenInsFunc = "() values ()"
        Me.RecMove1.genupdfunc = ""
        Me.RecMove1.Location = New System.Drawing.Point(0, 565)
        Me.RecMove1.Name = "RecMove1"
        Me.RecMove1.Setds = Nothing
        Me.RecMove1.Setpos = 0
        Me.RecMove1.Size = New System.Drawing.Size(984, 46)
        Me.RecMove1.TabIndex = 0
        '
        'ACCNAME
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.vxtEndNo)
        Me.Controls.Add(Me.vxtStartNo)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RecMove1)
        Me.Name = "ACCNAME"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.nudOutYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        CType(Me.nudOutYearT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RecMove1 As LAEACC_AP.RecMove
    Friend WithEvents ChkBelong As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents cboAccno As System.Windows.Forms.ComboBox
    Friend WithEvents lblFinish As System.Windows.Forms.Label
    Friend WithEvents btnBook As System.Windows.Forms.Button
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoOutyearNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOutyearYes As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoExcel As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPreview As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPrint As System.Windows.Forms.RadioButton
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblMsgOutyear As System.Windows.Forms.Label
    Friend WithEvents btnOutyear As System.Windows.Forms.Button
    Friend WithEvents nudOutYearT As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BtnSearch As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents txtQunit As System.Windows.Forms.TextBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents txtQaccount_no As System.Windows.Forms.TextBox
    Friend WithEvents txtQstaff_no As System.Windows.Forms.TextBox
    Friend WithEvents txtQaccname As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents nudOutYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label21 As System.Windows.Forms.Label
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
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblAccno As System.Windows.Forms.Label
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBelong As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAccount_no As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtStaff_no As System.Windows.Forms.TextBox
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents txtAccname As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents vxtStartNo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtEndNo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtAccnoEnd As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtAccnoStart As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtBook As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtAccno As System.Windows.Forms.MaskedTextBox

End Class
