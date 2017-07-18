<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PAY080
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
        Me.lblPage = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtPrintPageS = New System.Windows.Forms.TextBox()
        Me.txtEbank = New System.Windows.Forms.TextBox()
        Me.txtSbank = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rdoPreview = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rdoPrint = New System.Windows.Forms.RadioButton()
        Me.txtNo = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
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
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtPrintPageE)
        Me.GroupBox2.Controls.Add(Me.rdbPage)
        Me.GroupBox2.Controls.Add(Me.rdbAll)
        Me.GroupBox2.Controls.Add(Me.txtPrintPageS)
        Me.GroupBox2.Controls.Add(Me.lblPage)
        Me.GroupBox2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(230, 185)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(392, 64)
        Me.GroupBox2.TabIndex = 43
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "列表機列印範圍"
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
        'txtEbank
        '
        Me.txtEbank.Location = New System.Drawing.Point(318, 89)
        Me.txtEbank.Name = "txtEbank"
        Me.txtEbank.Size = New System.Drawing.Size(40, 29)
        Me.txtEbank.TabIndex = 42
        Me.txtEbank.Text = "99"
        '
        'txtSbank
        '
        Me.txtSbank.Location = New System.Drawing.Point(318, 57)
        Me.txtSbank.Name = "txtSbank"
        Me.txtSbank.Size = New System.Drawing.Size(40, 29)
        Me.txtSbank.TabIndex = 41
        Me.txtSbank.Text = "01"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.Location = New System.Drawing.Point(238, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 24)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "訖印銀行"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(238, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 24)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "起印銀行"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rdoPreview
        '
        Me.rdoPreview.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPreview.ForeColor = System.Drawing.Color.Green
        Me.rdoPreview.Location = New System.Drawing.Point(192, 32)
        Me.rdoPreview.Name = "rdoPreview"
        Me.rdoPreview.Size = New System.Drawing.Size(96, 24)
        Me.rdoPreview.TabIndex = 3
        Me.rdoPreview.Text = "預覽列印"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(390, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 24)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "訖印日期"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rdoPrint
        '
        Me.rdoPrint.Checked = True
        Me.rdoPrint.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPrint.ForeColor = System.Drawing.Color.Green
        Me.rdoPrint.Location = New System.Drawing.Point(88, 32)
        Me.rdoPrint.Name = "rdoPrint"
        Me.rdoPrint.Size = New System.Drawing.Size(96, 24)
        Me.rdoPrint.TabIndex = 2
        Me.rdoPrint.TabStop = True
        Me.rdoPrint.Text = "直接列印"
        '
        'txtNo
        '
        Me.txtNo.Location = New System.Drawing.Point(318, 137)
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(40, 29)
        Me.txtNo.TabIndex = 35
        Me.txtNo.Text = "1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoPreview)
        Me.GroupBox1.Controls.Add(Me.rdoPrint)
        Me.GroupBox1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(262, 257)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 64)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "列印方式"
        '
        'dtpDateE
        '
        Me.dtpDateE.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(470, 97)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(120, 27)
        Me.dtpDateE.TabIndex = 36
        '
        'dtpDateS
        '
        Me.dtpDateS.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS.Location = New System.Drawing.Point(470, 57)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(120, 27)
        Me.dtpDateS.TabIndex = 34
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(254, 137)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 22)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "起頁"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("新細明體", 17.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Green
        Me.Label18.Location = New System.Drawing.Point(318, 9)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(240, 23)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "列印存款明細分戶帳 "
        '
        'BtnPrint
        '
        Me.BtnPrint.BackColor = System.Drawing.Color.Red
        Me.BtnPrint.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(326, 337)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(64, 33)
        Me.BtnPrint.TabIndex = 31
        Me.BtnPrint.Text = "列  印"
        Me.BtnPrint.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.Location = New System.Drawing.Point(438, 337)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 32)
        Me.btnExit.TabIndex = 30
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.Location = New System.Drawing.Point(390, 57)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 24)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "起印日期"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 603)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(984, 8)
        Me.StatusBar1.TabIndex = 28
        Me.StatusBar1.Text = "StatusBar1"
        '
        'PAY080
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtEbank)
        Me.Controls.Add(Me.txtSbank)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNo)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dtpDateE)
        Me.Controls.Add(Me.dtpDateS)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.StatusBar1)
        Me.Name = "PAY080"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPrintPageE As System.Windows.Forms.TextBox
    Friend WithEvents rdbPage As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAll As System.Windows.Forms.RadioButton
    Friend WithEvents lblPage As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPrintPageS As System.Windows.Forms.TextBox
    Friend WithEvents txtEbank As System.Windows.Forms.TextBox
    Friend WithEvents txtSbank As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdoPreview As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rdoPrint As System.Windows.Forms.RadioButton
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar

End Class
