<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BG030
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lblNoYear = New System.Windows.Forms.Label()
        Me.btnNo = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNO = New System.Windows.Forms.TextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.btnReflesh = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblUseAmt = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblUseableAmt = New System.Windows.Forms.Label()
        Me.lblAmt3 = New System.Windows.Forms.Label()
        Me.lblAmt2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtAmt3 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblUnUseamt = New System.Windows.Forms.Label()
        Me.lblBgamt = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblAmt1 = New System.Windows.Forms.Label()
        Me.lblDate1 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnSure = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbkind2 = New System.Windows.Forms.RadioButton()
        Me.rdbKind1 = New System.Windows.Forms.RadioButton()
        Me.lblAccno = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.lblBgno = New System.Windows.Forms.Label()
        Me.txtAmt2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblAccname = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblkey = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabControl1.ItemSize = New System.Drawing.Size(76, 21)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(768, 536)
        Me.TabControl1.TabIndex = 5
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.lblNoYear)
        Me.TabPage1.Controls.Add(Me.btnNo)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.txtNO)
        Me.TabPage1.Controls.Add(Me.btnExit)
        Me.TabPage1.Controls.Add(Me.DataGrid1)
        Me.TabPage1.Controls.Add(Me.btnReflesh)
        Me.TabPage1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(760, 507)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "請購中清單"
        '
        'lblNoYear
        '
        Me.lblNoYear.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblNoYear.Location = New System.Drawing.Point(352, 8)
        Me.lblNoYear.Name = "lblNoYear"
        Me.lblNoYear.Size = New System.Drawing.Size(40, 23)
        Me.lblNoYear.TabIndex = 20
        Me.lblNoYear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnNo
        '
        Me.btnNo.BackColor = System.Drawing.Color.Lime
        Me.btnNo.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnNo.Location = New System.Drawing.Point(456, 8)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(56, 32)
        Me.btnNo.TabIndex = 19
        Me.btnNo.Text = "確定"
        Me.btnNo.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label18.Location = New System.Drawing.Point(256, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(100, 23)
        Me.Label18.TabIndex = 18
        Me.Label18.Text = "請購編號:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNO
        '
        Me.txtNO.Location = New System.Drawing.Point(392, 8)
        Me.txtNO.MaxLength = 5
        Me.txtNO.Name = "txtNO"
        Me.txtNO.Size = New System.Drawing.Size(56, 27)
        Me.txtNO.TabIndex = 16
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.Location = New System.Drawing.Point(664, 8)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 32)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'DataGrid1
        '
        Me.DataGrid1.CaptionText = "請購中清單(雙點審核)"
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.DataGrid1.HeaderFont = New System.Drawing.Font("新細明體", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(0, 40)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(760, 464)
        Me.DataGrid1.TabIndex = 3
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGrid1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn5})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "bgf020"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "請購編號"
        Me.DataGridTextBoxColumn1.MappingName = "bgno"
        Me.DataGridTextBoxColumn1.Width = 90
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "請購日"
        Me.DataGridTextBoxColumn4.MappingName = "date1"
        Me.DataGridTextBoxColumn4.Width = 95
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "年度"
        Me.DataGridTextBoxColumn2.MappingName = "accyear"
        Me.DataGridTextBoxColumn2.Width = 30
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "科目"
        Me.DataGridTextBoxColumn3.MappingName = "accno"
        Me.DataGridTextBoxColumn3.Width = 160
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "科目名稱"
        Me.DataGridTextBoxColumn7.MappingName = "accname"
        Me.DataGridTextBoxColumn7.Width = 300
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn6.Format = "###,###,###"
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "請購金額"
        Me.DataGridTextBoxColumn6.MappingName = "amt1"
        Me.DataGridTextBoxColumn6.Width = 90
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn8.Format = "###,###,###"
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "發包"
        Me.DataGridTextBoxColumn8.MappingName = "amt2"
        Me.DataGridTextBoxColumn8.Width = 90
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn9.Format = "###,###,###"
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "變更"
        Me.DataGridTextBoxColumn9.MappingName = "amt3"
        Me.DataGridTextBoxColumn9.Width = 90
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "事由"
        Me.DataGridTextBoxColumn5.MappingName = "remark"
        Me.DataGridTextBoxColumn5.Width = 230
        '
        'btnReflesh
        '
        Me.btnReflesh.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnReflesh.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnReflesh.ForeColor = System.Drawing.Color.Red
        Me.btnReflesh.Location = New System.Drawing.Point(8, 8)
        Me.btnReflesh.Name = "btnReflesh"
        Me.btnReflesh.Size = New System.Drawing.Size(96, 32)
        Me.btnReflesh.TabIndex = 2
        Me.btnReflesh.Text = "重新整理"
        Me.btnReflesh.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.Label15)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.lblUseAmt)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.lblUseableAmt)
        Me.TabPage2.Controls.Add(Me.lblAmt3)
        Me.TabPage2.Controls.Add(Me.lblAmt2)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.txtAmt3)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.lblRemark)
        Me.TabPage2.Controls.Add(Me.lblUnUseamt)
        Me.TabPage2.Controls.Add(Me.lblBgamt)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.lblAmt1)
        Me.TabPage2.Controls.Add(Me.lblDate1)
        Me.TabPage2.Controls.Add(Me.btnBack)
        Me.TabPage2.Controls.Add(Me.btnSure)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.lblAccno)
        Me.TabPage2.Controls.Add(Me.lblYear)
        Me.TabPage2.Controls.Add(Me.lblBgno)
        Me.TabPage2.Controls.Add(Me.txtAmt2)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.lblAccname)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.lblkey)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabPage2.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(976, 566)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "單筆明細審核"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Green
        Me.Label15.Location = New System.Drawing.Point(392, 224)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(88, 24)
        Me.Label15.TabIndex = 76
        Me.Label15.Text = "可支用:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Green
        Me.Label9.Location = New System.Drawing.Point(392, 304)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 24)
        Me.Label9.TabIndex = 75
        Me.Label9.Text = "原變更:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Green
        Me.Label14.Location = New System.Drawing.Point(392, 264)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 24)
        Me.Label14.TabIndex = 74
        Me.Label14.Text = "原發包:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblUseAmt
        '
        Me.lblUseAmt.Location = New System.Drawing.Point(480, 344)
        Me.lblUseAmt.Name = "lblUseAmt"
        Me.lblUseAmt.Size = New System.Drawing.Size(128, 23)
        Me.lblUseAmt.TabIndex = 73
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Green
        Me.Label13.Location = New System.Drawing.Point(336, 344)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(144, 24)
        Me.Label13.TabIndex = 72
        Me.Label13.Text = "本筆已開支額="
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblUseableAmt
        '
        Me.lblUseableAmt.Location = New System.Drawing.Point(488, 224)
        Me.lblUseableAmt.Name = "lblUseableAmt"
        Me.lblUseableAmt.Size = New System.Drawing.Size(120, 23)
        Me.lblUseableAmt.TabIndex = 71
        Me.lblUseableAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAmt3
        '
        Me.lblAmt3.Location = New System.Drawing.Point(488, 304)
        Me.lblAmt3.Name = "lblAmt3"
        Me.lblAmt3.Size = New System.Drawing.Size(120, 23)
        Me.lblAmt3.TabIndex = 70
        Me.lblAmt3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAmt2
        '
        Me.lblAmt2.Location = New System.Drawing.Point(488, 264)
        Me.lblAmt2.Name = "lblAmt2"
        Me.lblAmt2.Size = New System.Drawing.Size(120, 23)
        Me.lblAmt2.TabIndex = 69
        Me.lblAmt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label8.Location = New System.Drawing.Point(56, 296)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 24)
        Me.Label8.TabIndex = 68
        Me.Label8.Text = "變更金額"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAmt3
        '
        Me.txtAmt3.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmt3.Location = New System.Drawing.Point(176, 296)
        Me.txtAmt3.Name = "txtAmt3"
        Me.txtAmt3.Size = New System.Drawing.Size(128, 29)
        Me.txtAmt3.TabIndex = 2
        Me.txtAmt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label7.Location = New System.Drawing.Point(56, 256)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 24)
        Me.Label7.TabIndex = 66
        Me.Label7.Text = "發包金額"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(176, 192)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(520, 23)
        Me.lblRemark.TabIndex = 65
        Me.lblRemark.Text = "lblRemark false"
        '
        'lblUnUseamt
        '
        Me.lblUnUseamt.Location = New System.Drawing.Point(480, 408)
        Me.lblUnUseamt.Name = "lblUnUseamt"
        Me.lblUnUseamt.Size = New System.Drawing.Size(128, 23)
        Me.lblUnUseamt.TabIndex = 64
        '
        'lblBgamt
        '
        Me.lblBgamt.Location = New System.Drawing.Point(480, 376)
        Me.lblBgamt.Name = "lblBgamt"
        Me.lblBgamt.Size = New System.Drawing.Size(128, 23)
        Me.lblBgamt.TabIndex = 63
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Green
        Me.Label12.Location = New System.Drawing.Point(336, 408)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(144, 24)
        Me.Label12.TabIndex = 62
        Me.Label12.Text = "科目開支餘額="
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Green
        Me.Label11.Location = New System.Drawing.Point(336, 376)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(144, 24)
        Me.Label11.TabIndex = 61
        Me.Label11.Text = "預算科目餘額="
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmt1
        '
        Me.lblAmt1.Location = New System.Drawing.Point(184, 224)
        Me.lblAmt1.Name = "lblAmt1"
        Me.lblAmt1.Size = New System.Drawing.Size(120, 23)
        Me.lblAmt1.TabIndex = 55
        Me.lblAmt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDate1
        '
        Me.lblDate1.Location = New System.Drawing.Point(176, 160)
        Me.lblDate1.Name = "lblDate1"
        Me.lblDate1.Size = New System.Drawing.Size(100, 23)
        Me.lblDate1.TabIndex = 54
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnBack.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.Red
        Me.btnBack.Location = New System.Drawing.Point(248, 368)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(64, 32)
        Me.btnBack.TabIndex = 4
        Me.btnBack.Text = "退回"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnSure
        '
        Me.btnSure.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSure.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSure.ForeColor = System.Drawing.Color.Red
        Me.btnSure.Location = New System.Drawing.Point(152, 368)
        Me.btnSure.Name = "btnSure"
        Me.btnSure.Size = New System.Drawing.Size(64, 32)
        Me.btnSure.TabIndex = 3
        Me.btnSure.Text = "確定"
        Me.btnSure.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rdbkind2)
        Me.GroupBox1.Controls.Add(Me.rdbKind1)
        Me.GroupBox1.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(176, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 56)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        '
        'rdbkind2
        '
        Me.rdbkind2.Checked = True
        Me.rdbkind2.Enabled = False
        Me.rdbkind2.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbkind2.ForeColor = System.Drawing.Color.Red
        Me.rdbkind2.Location = New System.Drawing.Point(72, 24)
        Me.rdbkind2.Name = "rdbkind2"
        Me.rdbkind2.Size = New System.Drawing.Size(64, 24)
        Me.rdbkind2.TabIndex = 6
        Me.rdbkind2.TabStop = True
        Me.rdbkind2.Text = "支出"
        '
        'rdbKind1
        '
        Me.rdbKind1.Enabled = False
        Me.rdbKind1.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbKind1.ForeColor = System.Drawing.Color.Red
        Me.rdbKind1.Location = New System.Drawing.Point(8, 24)
        Me.rdbKind1.Name = "rdbKind1"
        Me.rdbKind1.Size = New System.Drawing.Size(64, 24)
        Me.rdbKind1.TabIndex = 5
        Me.rdbKind1.Text = "收入"
        '
        'lblAccno
        '
        Me.lblAccno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblAccno.Location = New System.Drawing.Point(320, 56)
        Me.lblAccno.Name = "lblAccno"
        Me.lblAccno.Size = New System.Drawing.Size(200, 24)
        Me.lblAccno.TabIndex = 32
        Me.lblAccno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblYear
        '
        Me.lblYear.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblYear.Location = New System.Drawing.Point(184, 56)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(48, 24)
        Me.lblYear.TabIndex = 31
        Me.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBgno
        '
        Me.lblBgno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblBgno.ForeColor = System.Drawing.Color.Red
        Me.lblBgno.Location = New System.Drawing.Point(184, 24)
        Me.lblBgno.Name = "lblBgno"
        Me.lblBgno.Size = New System.Drawing.Size(120, 24)
        Me.lblBgno.TabIndex = 30
        Me.lblBgno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAmt2
        '
        Me.txtAmt2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmt2.Location = New System.Drawing.Point(176, 256)
        Me.txtAmt2.Name = "txtAmt2"
        Me.txtAmt2.Size = New System.Drawing.Size(128, 29)
        Me.txtAmt2.TabIndex = 1
        Me.txtAmt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label6.Location = New System.Drawing.Point(56, 224)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 24)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "請購金額"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label4.Location = New System.Drawing.Point(56, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 24)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "請購事由"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label3.Location = New System.Drawing.Point(32, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 24)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "單位請購日期"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label2.Location = New System.Drawing.Point(88, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 24)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "傳票"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label1.Location = New System.Drawing.Point(88, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 24)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "請購編號"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAccname
        '
        Me.lblAccname.ForeColor = System.Drawing.Color.Green
        Me.lblAccname.Location = New System.Drawing.Point(312, 80)
        Me.lblAccname.Name = "lblAccname"
        Me.lblAccname.Size = New System.Drawing.Size(296, 23)
        Me.lblAccname.TabIndex = 19
        Me.lblAccname.Text = "lblAccname"
        Me.lblAccname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label10.Location = New System.Drawing.Point(128, 56)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 24)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "年度"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblkey
        '
        Me.lblkey.Location = New System.Drawing.Point(536, 8)
        Me.lblkey.Name = "lblkey"
        Me.lblkey.Size = New System.Drawing.Size(80, 23)
        Me.lblkey.TabIndex = 15
        Me.lblkey.Text = "lblkey"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label5.Location = New System.Drawing.Point(240, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "請購科目"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 595)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(984, 16)
        Me.StatusBar1.TabIndex = 6
        Me.StatusBar1.Text = "StatusBar1"
        '
        'BG030
        '
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusBar1)
        Me.Name = "BG030"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents lblNoYear As System.Windows.Forms.Label
    Friend WithEvents btnNo As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtNO As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnReflesh As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblUseAmt As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblUseableAmt As System.Windows.Forms.Label
    Friend WithEvents lblAmt3 As System.Windows.Forms.Label
    Friend WithEvents lblAmt2 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtAmt3 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents lblUnUseamt As System.Windows.Forms.Label
    Friend WithEvents lblBgamt As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblAmt1 As System.Windows.Forms.Label
    Friend WithEvents lblDate1 As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnSure As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbkind2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbKind1 As System.Windows.Forms.RadioButton
    Friend WithEvents lblAccno As System.Windows.Forms.Label
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents lblBgno As System.Windows.Forms.Label
    Friend WithEvents txtAmt2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblAccname As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblkey As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar

End Class
