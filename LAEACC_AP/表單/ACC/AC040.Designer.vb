<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AC040
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
        Me.lblDate_1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.lblNo_1_no = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCode6 = New System.Windows.Forms.TextBox()
        Me.txtCode5 = New System.Windows.Forms.TextBox()
        Me.txtAmt5 = New System.Windows.Forms.TextBox()
        Me.txtCode4 = New System.Windows.Forms.TextBox()
        Me.txtAmt4 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblUseNO = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtAmt6 = New System.Windows.Forms.TextBox()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.txtCode3 = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dtgSource = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.vxtAccno6 = New System.Windows.Forms.MaskedTextBox()
        Me.vxtAccno5 = New System.Windows.Forms.MaskedTextBox()
        Me.vxtAccno4 = New System.Windows.Forms.MaskedTextBox()
        Me.vxtAccno3 = New System.Windows.Forms.MaskedTextBox()
        Me.vxtAccno2 = New System.Windows.Forms.MaskedTextBox()
        Me.vxtAccno1 = New System.Windows.Forms.MaskedTextBox()
        Me.txtAmt3 = New System.Windows.Forms.TextBox()
        Me.txtCode2 = New System.Windows.Forms.TextBox()
        Me.txtAmt2 = New System.Windows.Forms.TextBox()
        Me.txtAmt1 = New System.Windows.Forms.TextBox()
        Me.txtRemark6 = New System.Windows.Forms.TextBox()
        Me.txtRemark5 = New System.Windows.Forms.TextBox()
        Me.txtRemark4 = New System.Windows.Forms.TextBox()
        Me.txtRemark3 = New System.Windows.Forms.TextBox()
        Me.txtRemark2 = New System.Windows.Forms.TextBox()
        Me.txtRemark1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNo_2_no = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dtgSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDate_1
        '
        Me.lblDate_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDate_1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDate_1.ForeColor = System.Drawing.Color.Blue
        Me.lblDate_1.Location = New System.Drawing.Point(112, 48)
        Me.lblDate_1.Name = "lblDate_1"
        Me.lblDate_1.Size = New System.Drawing.Size(104, 24)
        Me.lblDate_1.TabIndex = 48
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(24, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 24)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "製票日期"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Button2.Location = New System.Drawing.Point(744, 24)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 32)
        Me.Button2.TabIndex = 46
        Me.Button2.Text = "回上頁"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.Red
        Me.btnFinish.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnFinish.Location = New System.Drawing.Point(456, 64)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(56, 32)
        Me.btnFinish.TabIndex = 41
        Me.btnFinish.Text = "確定"
        Me.btnFinish.UseVisualStyleBackColor = False
        '
        'lblNo_1_no
        '
        Me.lblNo_1_no.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblNo_1_no.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblNo_1_no.ForeColor = System.Drawing.Color.Blue
        Me.lblNo_1_no.Location = New System.Drawing.Point(128, 80)
        Me.lblNo_1_no.Name = "lblNo_1_no"
        Me.lblNo_1_no.Size = New System.Drawing.Size(88, 24)
        Me.lblNo_1_no.TabIndex = 40
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(40, 80)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 24)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "製票編號"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(896, 152)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 56)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "內容別"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("新細明體", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(776, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 24)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "金      額"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("新細明體", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(256, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(496, 24)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "         摘                     要                                             -"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("新細明體", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(48, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 24)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "會計科目"
        '
        'txtCode6
        '
        Me.txtCode6.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCode6.Location = New System.Drawing.Point(896, 432)
        Me.txtCode6.MaxLength = 1
        Me.txtCode6.Name = "txtCode6"
        Me.txtCode6.Size = New System.Drawing.Size(32, 22)
        Me.txtCode6.TabIndex = 27
        Me.txtCode6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCode5
        '
        Me.txtCode5.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCode5.Location = New System.Drawing.Point(896, 376)
        Me.txtCode5.MaxLength = 1
        Me.txtCode5.Name = "txtCode5"
        Me.txtCode5.Size = New System.Drawing.Size(32, 22)
        Me.txtCode5.TabIndex = 25
        Me.txtCode5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAmt5
        '
        Me.txtAmt5.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmt5.Location = New System.Drawing.Point(760, 376)
        Me.txtAmt5.Name = "txtAmt5"
        Me.txtAmt5.Size = New System.Drawing.Size(128, 29)
        Me.txtAmt5.TabIndex = 24
        Me.txtAmt5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCode4
        '
        Me.txtCode4.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCode4.Location = New System.Drawing.Point(896, 320)
        Me.txtCode4.MaxLength = 1
        Me.txtCode4.Name = "txtCode4"
        Me.txtCode4.Size = New System.Drawing.Size(32, 22)
        Me.txtCode4.TabIndex = 23
        Me.txtCode4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAmt4
        '
        Me.txtAmt4.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmt4.Location = New System.Drawing.Point(760, 320)
        Me.txtAmt4.Name = "txtAmt4"
        Me.txtAmt4.Size = New System.Drawing.Size(128, 29)
        Me.txtAmt4.TabIndex = 22
        Me.txtAmt4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Button1.Location = New System.Drawing.Point(707, 9)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(64, 32)
        Me.Button1.TabIndex = 51
        Me.Button1.Text = "結束"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(19, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 24)
        Me.Label9.TabIndex = 50
        Me.Label9.Text = "年度="
        '
        'lblUseNO
        '
        Me.lblUseNO.ForeColor = System.Drawing.Color.Red
        Me.lblUseNO.Location = New System.Drawing.Point(275, 9)
        Me.lblUseNO.Name = "lblUseNO"
        Me.lblUseNO.Size = New System.Drawing.Size(72, 23)
        Me.lblUseNO.TabIndex = 48
        Me.lblUseNO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(139, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(136, 24)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "上張轉帳編號："
        '
        'txtAmt6
        '
        Me.txtAmt6.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmt6.Location = New System.Drawing.Point(760, 432)
        Me.txtAmt6.Name = "txtAmt6"
        Me.txtAmt6.Size = New System.Drawing.Size(128, 29)
        Me.txtAmt6.TabIndex = 26
        Me.txtAmt6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblYear
        '
        Me.lblYear.Location = New System.Drawing.Point(67, 9)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(64, 24)
        Me.lblYear.TabIndex = 52
        '
        'txtCode3
        '
        Me.txtCode3.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCode3.Location = New System.Drawing.Point(896, 264)
        Me.txtCode3.MaxLength = 1
        Me.txtCode3.Name = "txtCode3"
        Me.txtCode3.Size = New System.Drawing.Size(32, 22)
        Me.txtCode3.TabIndex = 21
        Me.txtCode3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(11, 41)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(944, 544)
        Me.TabControl1.TabIndex = 47
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.dtgSource)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(936, 511)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "資料來源"
        '
        'dtgSource
        '
        Me.dtgSource.CaptionText = "(雙點至決裁選入區)"
        Me.dtgSource.DataMember = ""
        Me.dtgSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtgSource.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dtgSource.Location = New System.Drawing.Point(0, 0)
        Me.dtgSource.Name = "dtgSource"
        Me.dtgSource.Size = New System.Drawing.Size(936, 511)
        Me.dtgSource.TabIndex = 9
        Me.dtgSource.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle2})
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.DataGrid = Me.dtgSource
        Me.DataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn12})
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle2.MappingName = "ac010S"
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "製票編號"
        Me.DataGridTextBoxColumn6.MappingName = "no_1_no"
        Me.DataGridTextBoxColumn6.Width = 80
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "會計科目"
        Me.DataGridTextBoxColumn7.MappingName = "accno"
        Me.DataGridTextBoxColumn7.Width = 140
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "摘要"
        Me.DataGridTextBoxColumn8.MappingName = "remark"
        Me.DataGridTextBoxColumn8.Width = 300
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn9.Format = "###,###,###,###.##"
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "金額"
        Me.DataGridTextBoxColumn9.MappingName = "amt"
        Me.DataGridTextBoxColumn9.Width = 120
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.MappingName = "autono"
        Me.DataGridTextBoxColumn12.Width = 75
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.vxtAccno6)
        Me.TabPage2.Controls.Add(Me.vxtAccno5)
        Me.TabPage2.Controls.Add(Me.vxtAccno4)
        Me.TabPage2.Controls.Add(Me.vxtAccno3)
        Me.TabPage2.Controls.Add(Me.vxtAccno2)
        Me.TabPage2.Controls.Add(Me.vxtAccno1)
        Me.TabPage2.Controls.Add(Me.lblDate_1)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Button2)
        Me.TabPage2.Controls.Add(Me.btnFinish)
        Me.TabPage2.Controls.Add(Me.lblNo_1_no)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.txtCode6)
        Me.TabPage2.Controls.Add(Me.txtAmt6)
        Me.TabPage2.Controls.Add(Me.txtCode5)
        Me.TabPage2.Controls.Add(Me.txtAmt5)
        Me.TabPage2.Controls.Add(Me.txtCode4)
        Me.TabPage2.Controls.Add(Me.txtAmt4)
        Me.TabPage2.Controls.Add(Me.txtCode3)
        Me.TabPage2.Controls.Add(Me.txtAmt3)
        Me.TabPage2.Controls.Add(Me.txtCode2)
        Me.TabPage2.Controls.Add(Me.txtAmt2)
        Me.TabPage2.Controls.Add(Me.txtAmt1)
        Me.TabPage2.Controls.Add(Me.txtRemark6)
        Me.TabPage2.Controls.Add(Me.txtRemark5)
        Me.TabPage2.Controls.Add(Me.txtRemark4)
        Me.TabPage2.Controls.Add(Me.txtRemark3)
        Me.TabPage2.Controls.Add(Me.txtRemark2)
        Me.TabPage2.Controls.Add(Me.txtRemark1)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.dtpDate)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.lblNo_2_no)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(936, 511)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "傳票決裁"
        Me.TabPage2.ToolTipText = "由來源資料轉入傳票,完成後並列印傳票"
        '
        'vxtAccno6
        '
        Me.vxtAccno6.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccno6.Location = New System.Drawing.Point(56, 432)
        Me.vxtAccno6.Mask = "#-####-##-##-#######-#"
        Me.vxtAccno6.Name = "vxtAccno6"
        Me.vxtAccno6.Size = New System.Drawing.Size(179, 25)
        Me.vxtAccno6.TabIndex = 54
        '
        'vxtAccno5
        '
        Me.vxtAccno5.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccno5.Location = New System.Drawing.Point(56, 376)
        Me.vxtAccno5.Mask = "#-####-##-##-#######-#"
        Me.vxtAccno5.Name = "vxtAccno5"
        Me.vxtAccno5.Size = New System.Drawing.Size(179, 25)
        Me.vxtAccno5.TabIndex = 53
        '
        'vxtAccno4
        '
        Me.vxtAccno4.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccno4.Location = New System.Drawing.Point(56, 320)
        Me.vxtAccno4.Mask = "#-####-##-##-#######-#"
        Me.vxtAccno4.Name = "vxtAccno4"
        Me.vxtAccno4.Size = New System.Drawing.Size(179, 25)
        Me.vxtAccno4.TabIndex = 52
        '
        'vxtAccno3
        '
        Me.vxtAccno3.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccno3.Location = New System.Drawing.Point(56, 264)
        Me.vxtAccno3.Mask = "#-####-##-##-#######-#"
        Me.vxtAccno3.Name = "vxtAccno3"
        Me.vxtAccno3.Size = New System.Drawing.Size(179, 25)
        Me.vxtAccno3.TabIndex = 51
        '
        'vxtAccno2
        '
        Me.vxtAccno2.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccno2.Location = New System.Drawing.Point(56, 208)
        Me.vxtAccno2.Mask = "#-####-##-##-#######-#"
        Me.vxtAccno2.Name = "vxtAccno2"
        Me.vxtAccno2.Size = New System.Drawing.Size(179, 25)
        Me.vxtAccno2.TabIndex = 50
        '
        'vxtAccno1
        '
        Me.vxtAccno1.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.vxtAccno1.Location = New System.Drawing.Point(56, 152)
        Me.vxtAccno1.Mask = "#-####"
        Me.vxtAccno1.Name = "vxtAccno1"
        Me.vxtAccno1.Size = New System.Drawing.Size(62, 25)
        Me.vxtAccno1.TabIndex = 49
        '
        'txtAmt3
        '
        Me.txtAmt3.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmt3.Location = New System.Drawing.Point(760, 264)
        Me.txtAmt3.Name = "txtAmt3"
        Me.txtAmt3.Size = New System.Drawing.Size(128, 29)
        Me.txtAmt3.TabIndex = 20
        Me.txtAmt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCode2
        '
        Me.txtCode2.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCode2.Location = New System.Drawing.Point(896, 208)
        Me.txtCode2.MaxLength = 1
        Me.txtCode2.Name = "txtCode2"
        Me.txtCode2.Size = New System.Drawing.Size(32, 22)
        Me.txtCode2.TabIndex = 19
        Me.txtCode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAmt2
        '
        Me.txtAmt2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmt2.Location = New System.Drawing.Point(760, 208)
        Me.txtAmt2.Name = "txtAmt2"
        Me.txtAmt2.Size = New System.Drawing.Size(128, 29)
        Me.txtAmt2.TabIndex = 18
        Me.txtAmt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAmt1
        '
        Me.txtAmt1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmt1.Location = New System.Drawing.Point(760, 152)
        Me.txtAmt1.Name = "txtAmt1"
        Me.txtAmt1.Size = New System.Drawing.Size(128, 29)
        Me.txtAmt1.TabIndex = 17
        Me.txtAmt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRemark6
        '
        Me.txtRemark6.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtRemark6.Location = New System.Drawing.Point(256, 432)
        Me.txtRemark6.MaxLength = 60
        Me.txtRemark6.Name = "txtRemark6"
        Me.txtRemark6.Size = New System.Drawing.Size(496, 25)
        Me.txtRemark6.TabIndex = 16
        '
        'txtRemark5
        '
        Me.txtRemark5.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtRemark5.Location = New System.Drawing.Point(256, 376)
        Me.txtRemark5.MaxLength = 60
        Me.txtRemark5.Name = "txtRemark5"
        Me.txtRemark5.Size = New System.Drawing.Size(496, 25)
        Me.txtRemark5.TabIndex = 15
        '
        'txtRemark4
        '
        Me.txtRemark4.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtRemark4.Location = New System.Drawing.Point(256, 320)
        Me.txtRemark4.MaxLength = 60
        Me.txtRemark4.Name = "txtRemark4"
        Me.txtRemark4.Size = New System.Drawing.Size(496, 25)
        Me.txtRemark4.TabIndex = 14
        '
        'txtRemark3
        '
        Me.txtRemark3.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtRemark3.Location = New System.Drawing.Point(256, 264)
        Me.txtRemark3.MaxLength = 60
        Me.txtRemark3.Name = "txtRemark3"
        Me.txtRemark3.Size = New System.Drawing.Size(496, 25)
        Me.txtRemark3.TabIndex = 13
        '
        'txtRemark2
        '
        Me.txtRemark2.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtRemark2.Location = New System.Drawing.Point(256, 208)
        Me.txtRemark2.MaxLength = 60
        Me.txtRemark2.Name = "txtRemark2"
        Me.txtRemark2.Size = New System.Drawing.Size(496, 25)
        Me.txtRemark2.TabIndex = 12
        '
        'txtRemark1
        '
        Me.txtRemark1.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtRemark1.Location = New System.Drawing.Point(256, 152)
        Me.txtRemark1.MaxLength = 60
        Me.txtRemark1.Name = "txtRemark1"
        Me.txtRemark1.Size = New System.Drawing.Size(496, 25)
        Me.txtRemark1.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(16, 216)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 136)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "明細帳"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(16, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 40)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "總帳"
        '
        'dtpDate
        '
        Me.dtpDate.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(392, 24)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(120, 30)
        Me.dtpDate.TabIndex = 0
        Me.dtpDate.Value = New Date(2005, 1, 30, 23, 47, 23, 931)
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(288, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "決裁日期"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNo_2_no
        '
        Me.lblNo_2_no.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblNo_2_no.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblNo_2_no.ForeColor = System.Drawing.Color.Red
        Me.lblNo_2_no.Location = New System.Drawing.Point(392, 72)
        Me.lblNo_2_no.Name = "lblNo_2_no"
        Me.lblNo_2_no.Size = New System.Drawing.Size(56, 23)
        Me.lblNo_2_no.TabIndex = 45
        Me.lblNo_2_no.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label12.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Red
        Me.Label12.Location = New System.Drawing.Point(296, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 24)
        Me.Label12.TabIndex = 45
        Me.Label12.Text = "轉帳編號"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AC040
        '
        Me.ClientSize = New System.Drawing.Size(987, 623)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblUseNO)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblYear)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "AC040"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dtgSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblDate_1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents lblNo_1_no As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCode6 As System.Windows.Forms.TextBox
    Friend WithEvents txtCode5 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmt5 As System.Windows.Forms.TextBox
    Friend WithEvents txtCode4 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmt4 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblUseNO As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtAmt6 As System.Windows.Forms.TextBox
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents txtCode3 As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dtgSource As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Protected WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtAmt3 As System.Windows.Forms.TextBox
    Friend WithEvents txtCode2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmt2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmt1 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark6 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark5 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark4 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark3 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark2 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblNo_2_no As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents vxtAccno6 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtAccno5 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtAccno4 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtAccno3 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtAccno2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtAccno1 As System.Windows.Forms.MaskedTextBox

End Class
