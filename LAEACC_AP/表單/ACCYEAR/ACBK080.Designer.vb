<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ACBK080
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
        Me.txtPrintPageE = New System.Windows.Forms.TextBox()
        Me.rdbPage = New System.Windows.Forms.RadioButton()
        Me.rdbAll = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.nudSmonth = New System.Windows.Forms.NumericUpDown()
        Me.nudSyear = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.nudEmonth = New System.Windows.Forms.NumericUpDown()
        Me.nudEyear = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rdoAccno2No = New System.Windows.Forms.RadioButton()
        Me.rdoAccno2Yes = New System.Windows.Forms.RadioButton()
        Me.txtPrintPageS = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblPage = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rdoPreview = New System.Windows.Forms.RadioButton()
        Me.rdoPrint = New System.Windows.Forms.RadioButton()
        Me.txtNo = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.vxtEAccno = New System.Windows.Forms.MaskedTextBox()
        Me.vxtSAccno = New System.Windows.Forms.MaskedTextBox()
        Me.GroupBox3.SuspendLayout()
        CType(Me.nudSmonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudSyear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.nudEmonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudEyear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtPrintPageE
        '
        Me.txtPrintPageE.Enabled = False
        Me.txtPrintPageE.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtPrintPageE.Location = New System.Drawing.Point(304, 24)
        Me.txtPrintPageE.Name = "txtPrintPageE"
        Me.txtPrintPageE.Size = New System.Drawing.Size(40, 25)
        Me.txtPrintPageE.TabIndex = 25
        Me.txtPrintPageE.Text = "999"
        '
        'rdbPage
        '
        Me.rdbPage.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbPage.ForeColor = System.Drawing.Color.Green
        Me.rdbPage.Location = New System.Drawing.Point(96, 24)
        Me.rdbPage.Name = "rdbPage"
        Me.rdbPage.Size = New System.Drawing.Size(64, 24)
        Me.rdbPage.TabIndex = 4
        Me.rdbPage.Text = "頁數"
        '
        'rdbAll
        '
        Me.rdbAll.Checked = True
        Me.rdbAll.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbAll.ForeColor = System.Drawing.Color.Green
        Me.rdbAll.Location = New System.Drawing.Point(16, 24)
        Me.rdbAll.Name = "rdbAll"
        Me.rdbAll.Size = New System.Drawing.Size(72, 24)
        Me.rdbAll.TabIndex = 3
        Me.rdbAll.TabStop = True
        Me.rdbAll.Text = "全部"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.nudSmonth)
        Me.GroupBox3.Controls.Add(Me.nudSyear)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(541, 41)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(200, 56)
        Me.GroupBox3.TabIndex = 49
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "起印"
        '
        'nudSmonth
        '
        Me.nudSmonth.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nudSmonth.Location = New System.Drawing.Point(112, 24)
        Me.nudSmonth.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nudSmonth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudSmonth.Name = "nudSmonth"
        Me.nudSmonth.Size = New System.Drawing.Size(48, 27)
        Me.nudSmonth.TabIndex = 35
        Me.nudSmonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudSmonth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudSyear
        '
        Me.nudSyear.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nudSyear.Location = New System.Drawing.Point(8, 24)
        Me.nudSyear.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.nudSyear.Name = "nudSyear"
        Me.nudSyear.Size = New System.Drawing.Size(56, 27)
        Me.nudSyear.TabIndex = 34
        Me.nudSyear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudSyear.Value = New Decimal(New Integer() {101, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.Location = New System.Drawing.Point(160, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(24, 24)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "月"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.Location = New System.Drawing.Point(64, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 24)
        Me.Label10.TabIndex = 32
        Me.Label10.Text = "年"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.nudEmonth)
        Me.GroupBox4.Controls.Add(Me.nudEyear)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(541, 97)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(200, 56)
        Me.GroupBox4.TabIndex = 50
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "訖印"
        '
        'nudEmonth
        '
        Me.nudEmonth.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nudEmonth.Location = New System.Drawing.Point(112, 24)
        Me.nudEmonth.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nudEmonth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudEmonth.Name = "nudEmonth"
        Me.nudEmonth.Size = New System.Drawing.Size(48, 27)
        Me.nudEmonth.TabIndex = 35
        Me.nudEmonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudEmonth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudEyear
        '
        Me.nudEyear.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nudEyear.Location = New System.Drawing.Point(8, 24)
        Me.nudEyear.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.nudEyear.Name = "nudEyear"
        Me.nudEyear.Size = New System.Drawing.Size(56, 27)
        Me.nudEyear.TabIndex = 34
        Me.nudEyear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudEyear.Value = New Decimal(New Integer() {101, 0, 0, 0})
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.Location = New System.Drawing.Point(160, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 24)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "月"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label12.Location = New System.Drawing.Point(64, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 24)
        Me.Label12.TabIndex = 32
        Me.Label12.Text = "年"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox5.Controls.Add(Me.rdoAccno2No)
        Me.GroupBox5.Controls.Add(Me.rdoAccno2Yes)
        Me.GroupBox5.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Red
        Me.GroupBox5.Location = New System.Drawing.Point(389, 177)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(352, 64)
        Me.GroupBox5.TabIndex = 51
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "累計折舊科目是否要列印?"
        '
        'rdoAccno2No
        '
        Me.rdoAccno2No.Checked = True
        Me.rdoAccno2No.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoAccno2No.ForeColor = System.Drawing.Color.Green
        Me.rdoAccno2No.Location = New System.Drawing.Point(224, 32)
        Me.rdoAccno2No.Name = "rdoAccno2No"
        Me.rdoAccno2No.Size = New System.Drawing.Size(96, 24)
        Me.rdoAccno2No.TabIndex = 3
        Me.rdoAccno2No.TabStop = True
        Me.rdoAccno2No.Text = "不列印"
        '
        'rdoAccno2Yes
        '
        Me.rdoAccno2Yes.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoAccno2Yes.ForeColor = System.Drawing.Color.Green
        Me.rdoAccno2Yes.Location = New System.Drawing.Point(120, 32)
        Me.rdoAccno2Yes.Name = "rdoAccno2Yes"
        Me.rdoAccno2Yes.Size = New System.Drawing.Size(96, 24)
        Me.rdoAccno2Yes.TabIndex = 2
        Me.rdoAccno2Yes.Text = "要列印"
        '
        'txtPrintPageS
        '
        Me.txtPrintPageS.Enabled = False
        Me.txtPrintPageS.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtPrintPageS.Location = New System.Drawing.Point(200, 24)
        Me.txtPrintPageS.Name = "txtPrintPageS"
        Me.txtPrintPageS.Size = New System.Drawing.Size(40, 25)
        Me.txtPrintPageS.TabIndex = 24
        Me.txtPrintPageS.Text = "1"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtPrintPageE)
        Me.GroupBox2.Controls.Add(Me.rdbPage)
        Me.GroupBox2.Controls.Add(Me.rdbAll)
        Me.GroupBox2.Controls.Add(Me.txtPrintPageS)
        Me.GroupBox2.Controls.Add(Me.lblPage)
        Me.GroupBox2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(293, 281)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(392, 64)
        Me.GroupBox2.TabIndex = 48
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "列表機列印範圍"
        '
        'lblPage
        '
        Me.lblPage.Enabled = False
        Me.lblPage.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblPage.Location = New System.Drawing.Point(168, 24)
        Me.lblPage.Name = "lblPage"
        Me.lblPage.Size = New System.Drawing.Size(216, 23)
        Me.lblPage.TabIndex = 5
        Me.lblPage.Text = "自                 頁至                   頁"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.Location = New System.Drawing.Point(221, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 24)
        Me.Label4.TabIndex = 47
        Me.Label4.Text = "訖印科目"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(221, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 24)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "起印科目"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rdoPreview
        '
        Me.rdoPreview.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPreview.ForeColor = System.Drawing.Color.Green
        Me.rdoPreview.Location = New System.Drawing.Point(112, 24)
        Me.rdoPreview.Name = "rdoPreview"
        Me.rdoPreview.Size = New System.Drawing.Size(96, 24)
        Me.rdoPreview.TabIndex = 3
        Me.rdoPreview.Text = "預覽列印"
        '
        'rdoPrint
        '
        Me.rdoPrint.Checked = True
        Me.rdoPrint.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPrint.ForeColor = System.Drawing.Color.Green
        Me.rdoPrint.Location = New System.Drawing.Point(8, 24)
        Me.rdoPrint.Name = "rdoPrint"
        Me.rdoPrint.Size = New System.Drawing.Size(96, 24)
        Me.rdoPrint.TabIndex = 2
        Me.rdoPrint.TabStop = True
        Me.rdoPrint.Text = "直接列印"
        '
        'txtNo
        '
        Me.txtNo.Location = New System.Drawing.Point(301, 241)
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(40, 29)
        Me.txtNo.TabIndex = 44
        Me.txtNo.Text = "1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoPreview)
        Me.GroupBox1.Controls.Add(Me.rdoPrint)
        Me.GroupBox1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(381, 353)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 64)
        Me.GroupBox1.TabIndex = 45
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "列印方式"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(237, 241)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 22)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "起頁"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("新細明體", 17.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Green
        Me.Label18.Location = New System.Drawing.Point(341, 9)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(344, 23)
        Me.Label18.TabIndex = 42
        Me.Label18.Text = "列印固定資產明細分戶帳"
        '
        'BtnPrint
        '
        Me.BtnPrint.BackColor = System.Drawing.Color.Red
        Me.BtnPrint.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(389, 433)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(64, 33)
        Me.BtnPrint.TabIndex = 41
        Me.BtnPrint.Text = "列  印"
        Me.BtnPrint.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.Location = New System.Drawing.Point(501, 433)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 32)
        Me.btnExit.TabIndex = 40
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 603)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(984, 8)
        Me.StatusBar1.TabIndex = 39
        Me.StatusBar1.Text = "StatusBar1"
        '
        'vxtEAccno
        '
        Me.vxtEAccno.Location = New System.Drawing.Point(301, 116)
        Me.vxtEAccno.Mask = "#-####-##-##-#######-#"
        Me.vxtEAccno.Name = "vxtEAccno"
        Me.vxtEAccno.Size = New System.Drawing.Size(179, 29)
        Me.vxtEAccno.TabIndex = 64
        '
        'vxtSAccno
        '
        Me.vxtSAccno.Location = New System.Drawing.Point(301, 68)
        Me.vxtSAccno.Mask = "#-####-##-##-#######-#"
        Me.vxtSAccno.Name = "vxtSAccno"
        Me.vxtSAccno.Size = New System.Drawing.Size(179, 29)
        Me.vxtSAccno.TabIndex = 63
        '
        'ACBK080
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.vxtEAccno)
        Me.Controls.Add(Me.vxtSAccno)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtNo)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.StatusBar1)
        Me.Name = "ACBK080"
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.nudSmonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudSyear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.nudEmonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudEyear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPrintPageE As System.Windows.Forms.TextBox
    Friend WithEvents rdbPage As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAll As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents nudSmonth As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudSyear As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents nudEmonth As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudEyear As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoAccno2No As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAccno2Yes As System.Windows.Forms.RadioButton
    Friend WithEvents txtPrintPageS As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblPage As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdoPreview As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPrint As System.Windows.Forms.RadioButton
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents vxtEAccno As System.Windows.Forms.MaskedTextBox
    Friend WithEvents vxtSAccno As System.Windows.Forms.MaskedTextBox

End Class
