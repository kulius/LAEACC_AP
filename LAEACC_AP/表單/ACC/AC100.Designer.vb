<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AC100
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
        Me.btnYear = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtEno3 = New System.Windows.Forms.TextBox()
        Me.txtSno3 = New System.Windows.Forms.TextBox()
        Me.txtSno2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPrintPageE = New System.Windows.Forms.TextBox()
        Me.rdbPage = New System.Windows.Forms.RadioButton()
        Me.rdbAll = New System.Windows.Forms.RadioButton()
        Me.txtPrintPageS = New System.Windows.Forms.TextBox()
        Me.txtEno2 = New System.Windows.Forms.TextBox()
        Me.txtEno1 = New System.Windows.Forms.TextBox()
        Me.lblPage = New System.Windows.Forms.Label()
        Me.nudYear = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.rdoPreview = New System.Windows.Forms.RadioButton()
        Me.rdoPrint = New System.Windows.Forms.RadioButton()
        Me.txtSno1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnYear
        '
        Me.btnYear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYear.Location = New System.Drawing.Point(436, 65)
        Me.btnYear.Name = "btnYear"
        Me.btnYear.Size = New System.Drawing.Size(96, 23)
        Me.btnYear.TabIndex = 74
        Me.btnYear.Text = "更換年度"
        Me.btnYear.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(308, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 24)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "年度"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEno3
        '
        Me.txtEno3.Location = New System.Drawing.Point(484, 193)
        Me.txtEno3.MaxLength = 5
        Me.txtEno3.Name = "txtEno3"
        Me.txtEno3.Size = New System.Drawing.Size(64, 29)
        Me.txtEno3.TabIndex = 60
        Me.txtEno3.Text = "0"
        Me.txtEno3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSno3
        '
        Me.txtSno3.Location = New System.Drawing.Point(372, 193)
        Me.txtSno3.MaxLength = 5
        Me.txtSno3.Name = "txtSno3"
        Me.txtSno3.Size = New System.Drawing.Size(64, 29)
        Me.txtSno3.TabIndex = 59
        Me.txtSno3.Text = "0"
        Me.txtSno3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSno2
        '
        Me.txtSno2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSno2.Location = New System.Drawing.Point(372, 153)
        Me.txtSno2.MaxLength = 5
        Me.txtSno2.Name = "txtSno2"
        Me.txtSno2.Size = New System.Drawing.Size(64, 29)
        Me.txtSno2.TabIndex = 56
        Me.txtSno2.Text = "0"
        Me.txtSno2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(444, 193)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 24)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "至"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(260, 193)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 24)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "轉帳傳票自"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(444, 153)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 24)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "至"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(260, 153)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 24)
        Me.Label5.TabIndex = 68
        Me.Label5.Text = "支出傳票自"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPrintPageE
        '
        Me.txtPrintPageE.Enabled = False
        Me.txtPrintPageE.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtPrintPageE.Location = New System.Drawing.Point(304, 24)
        Me.txtPrintPageE.Name = "txtPrintPageE"
        Me.txtPrintPageE.Size = New System.Drawing.Size(40, 25)
        Me.txtPrintPageE.TabIndex = 8
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
        'txtPrintPageS
        '
        Me.txtPrintPageS.Enabled = False
        Me.txtPrintPageS.Font = New System.Drawing.Font("新細明體", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtPrintPageS.Location = New System.Drawing.Point(200, 24)
        Me.txtPrintPageS.Name = "txtPrintPageS"
        Me.txtPrintPageS.Size = New System.Drawing.Size(40, 25)
        Me.txtPrintPageS.TabIndex = 7
        Me.txtPrintPageS.Text = "1"
        '
        'txtEno2
        '
        Me.txtEno2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEno2.Location = New System.Drawing.Point(484, 153)
        Me.txtEno2.MaxLength = 5
        Me.txtEno2.Name = "txtEno2"
        Me.txtEno2.Size = New System.Drawing.Size(64, 29)
        Me.txtEno2.TabIndex = 57
        Me.txtEno2.Text = "0"
        Me.txtEno2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEno1
        '
        Me.txtEno1.ForeColor = System.Drawing.Color.Red
        Me.txtEno1.Location = New System.Drawing.Point(484, 113)
        Me.txtEno1.MaxLength = 5
        Me.txtEno1.Name = "txtEno1"
        Me.txtEno1.Size = New System.Drawing.Size(64, 29)
        Me.txtEno1.TabIndex = 55
        Me.txtEno1.Text = "0"
        Me.txtEno1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        'nudYear
        '
        Me.nudYear.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nudYear.Location = New System.Drawing.Point(372, 65)
        Me.nudYear.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nudYear.Name = "nudYear"
        Me.nudYear.Size = New System.Drawing.Size(48, 27)
        Me.nudYear.TabIndex = 73
        Me.nudYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudYear.Value = New Decimal(New Integer() {101, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(444, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 24)
        Me.Label3.TabIndex = 67
        Me.Label3.Text = "至"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtPrintPageE)
        Me.GroupBox2.Controls.Add(Me.rdbPage)
        Me.GroupBox2.Controls.Add(Me.rdbAll)
        Me.GroupBox2.Controls.Add(Me.txtPrintPageS)
        Me.GroupBox2.Controls.Add(Me.lblPage)
        Me.GroupBox2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(252, 241)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(392, 64)
        Me.GroupBox2.TabIndex = 66
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "列表機列印範圍"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Red
        Me.Label15.Location = New System.Drawing.Point(260, 113)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(112, 24)
        Me.Label15.TabIndex = 65
        Me.Label15.Text = "收入傳票自"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        'txtSno1
        '
        Me.txtSno1.ForeColor = System.Drawing.Color.Red
        Me.txtSno1.Location = New System.Drawing.Point(372, 113)
        Me.txtSno1.MaxLength = 5
        Me.txtSno1.Name = "txtSno1"
        Me.txtSno1.Size = New System.Drawing.Size(64, 29)
        Me.txtSno1.TabIndex = 54
        Me.txtSno1.Text = "0"
        Me.txtSno1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoPreview)
        Me.GroupBox1.Controls.Add(Me.rdoPrint)
        Me.GroupBox1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(340, 321)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 64)
        Me.GroupBox1.TabIndex = 64
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "列印方式"
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("新細明體", 17.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Green
        Me.Label18.Location = New System.Drawing.Point(308, 25)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(328, 23)
        Me.Label18.TabIndex = 63
        Me.Label18.Text = "列印傳票交付簿 AC100 new"
        '
        'BtnPrint
        '
        Me.BtnPrint.BackColor = System.Drawing.Color.Red
        Me.BtnPrint.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(348, 393)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(64, 33)
        Me.BtnPrint.TabIndex = 62
        Me.BtnPrint.Text = "列  印"
        Me.BtnPrint.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.Location = New System.Drawing.Point(460, 393)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 32)
        Me.btnExit.TabIndex = 61
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 603)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(984, 8)
        Me.StatusBar1.TabIndex = 58
        Me.StatusBar1.Text = "StatusBar1"
        '
        'AC100
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.btnYear)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtEno3)
        Me.Controls.Add(Me.txtSno3)
        Me.Controls.Add(Me.txtSno2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtEno2)
        Me.Controls.Add(Me.txtEno1)
        Me.Controls.Add(Me.nudYear)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtSno1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.StatusBar1)
        Me.Name = "AC100"
        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnYear As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtEno3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSno3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSno2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPrintPageE As System.Windows.Forms.TextBox
    Friend WithEvents rdbPage As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAll As System.Windows.Forms.RadioButton
    Friend WithEvents txtPrintPageS As System.Windows.Forms.TextBox
    Friend WithEvents txtEno2 As System.Windows.Forms.TextBox
    Friend WithEvents txtEno1 As System.Windows.Forms.TextBox
    Friend WithEvents lblPage As System.Windows.Forms.Label
    Friend WithEvents nudYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents rdoPreview As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPrint As System.Windows.Forms.RadioButton
    Friend WithEvents txtSno1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar

End Class
