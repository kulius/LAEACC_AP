<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BAIL100
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
        Me.chkCop = New System.Windows.Forms.CheckBox()
        Me.chkEngno_4 = New System.Windows.Forms.CheckBox()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.pBar1 = New System.Windows.Forms.ProgressBar()
        Me.rdbSave = New System.Windows.Forms.RadioButton()
        Me.rdbPrint = New System.Windows.Forms.RadioButton()
        Me.chkAddEngno = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.rdbcourse2 = New System.Windows.Forms.RadioButton()
        Me.rdbcourse1 = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnPrt = New System.Windows.Forms.Button()
        Me.txtBgSub2 = New System.Windows.Forms.TextBox()
        Me.txtBgAdd2 = New System.Windows.Forms.TextBox()
        Me.txtBgSub1 = New System.Windows.Forms.TextBox()
        Me.txtBgAdd1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkCop
        '
        Me.chkCop.Checked = True
        Me.chkCop.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCop.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkCop.Location = New System.Drawing.Point(505, 337)
        Me.chkCop.Name = "chkCop"
        Me.chkCop.Size = New System.Drawing.Size(256, 24)
        Me.chkCop.TabIndex = 88
        Me.chkCop.Text = "科目名稱含廠商名稱或負責人"
        '
        'chkEngno_4
        '
        Me.chkEngno_4.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkEngno_4.Location = New System.Drawing.Point(505, 297)
        Me.chkEngno_4.Name = "chkEngno_4"
        Me.chkEngno_4.Size = New System.Drawing.Size(184, 24)
        Me.chkEngno_4.TabIndex = 86
        Me.chkEngno_4.Text = "依工程代號第4碼排序"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpEndDate.Location = New System.Drawing.Point(505, 57)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(200, 27)
        Me.dtpEndDate.TabIndex = 85
        '
        'pBar1
        '
        Me.pBar1.Location = New System.Drawing.Point(497, 457)
        Me.pBar1.Name = "pBar1"
        Me.pBar1.Size = New System.Drawing.Size(344, 23)
        Me.pBar1.TabIndex = 84
        Me.pBar1.Visible = False
        '
        'rdbSave
        '
        Me.rdbSave.Checked = True
        Me.rdbSave.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbSave.Location = New System.Drawing.Point(144, 24)
        Me.rdbSave.Name = "rdbSave"
        Me.rdbSave.Size = New System.Drawing.Size(64, 24)
        Me.rdbSave.TabIndex = 16
        Me.rdbSave.TabStop = True
        Me.rdbSave.Text = "存檔"
        '
        'rdbPrint
        '
        Me.rdbPrint.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbPrint.Location = New System.Drawing.Point(8, 24)
        Me.rdbPrint.Name = "rdbPrint"
        Me.rdbPrint.Size = New System.Drawing.Size(96, 24)
        Me.rdbPrint.TabIndex = 15
        Me.rdbPrint.Text = "直接列印"
        '
        'chkAddEngno
        '
        Me.chkAddEngno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkAddEngno.Location = New System.Drawing.Point(225, 257)
        Me.chkAddEngno.Name = "chkAddEngno"
        Me.chkAddEngno.Size = New System.Drawing.Size(184, 24)
        Me.chkAddEngno.TabIndex = 87
        Me.chkAddEngno.Text = "加上工程代號(核對用)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbSave)
        Me.GroupBox1.Controls.Add(Me.rdbPrint)
        Me.GroupBox1.Location = New System.Drawing.Point(497, 385)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(240, 64)
        Me.GroupBox1.TabIndex = 83
        Me.GroupBox1.TabStop = False
        '
        'btnEnd
        '
        Me.btnEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnEnd.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnEnd.Location = New System.Drawing.Point(633, 497)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(64, 32)
        Me.btnEnd.TabIndex = 82
        Me.btnEnd.Text = "結束"
        Me.btnEnd.UseVisualStyleBackColor = False
        '
        'rdbcourse2
        '
        Me.rdbcourse2.Checked = True
        Me.rdbcourse2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbcourse2.Location = New System.Drawing.Point(505, 265)
        Me.rdbcourse2.Name = "rdbcourse2"
        Me.rdbcourse2.Size = New System.Drawing.Size(208, 24)
        Me.rdbcourse2.TabIndex = 81
        Me.rdbcourse2.TabStop = True
        Me.rdbcourse2.Text = "保證品存單銀行、存單號"
        '
        'rdbcourse1
        '
        Me.rdbcourse1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbcourse1.Location = New System.Drawing.Point(505, 233)
        Me.rdbcourse1.Name = "rdbcourse1"
        Me.rdbcourse1.Size = New System.Drawing.Size(152, 24)
        Me.rdbcourse1.TabIndex = 80
        Me.rdbcourse1.Text = "保證品工程名稱"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label7.Location = New System.Drawing.Point(217, 233)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(280, 23)
        Me.Label7.TabIndex = 79
        Me.Label7.Text = "存入保證品明細在科目欄表示時,以："
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnPrt
        '
        Me.btnPrt.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnPrt.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnPrt.Location = New System.Drawing.Point(505, 497)
        Me.btnPrt.Name = "btnPrt"
        Me.btnPrt.Size = New System.Drawing.Size(64, 32)
        Me.btnPrt.TabIndex = 78
        Me.btnPrt.Text = "確定"
        Me.btnPrt.UseVisualStyleBackColor = False
        '
        'txtBgSub2
        '
        Me.txtBgSub2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtBgSub2.Location = New System.Drawing.Point(505, 185)
        Me.txtBgSub2.Name = "txtBgSub2"
        Me.txtBgSub2.Size = New System.Drawing.Size(112, 27)
        Me.txtBgSub2.TabIndex = 77
        Me.txtBgSub2.Text = "0"
        Me.txtBgSub2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBgAdd2
        '
        Me.txtBgAdd2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtBgAdd2.Location = New System.Drawing.Point(505, 153)
        Me.txtBgAdd2.Name = "txtBgAdd2"
        Me.txtBgAdd2.Size = New System.Drawing.Size(112, 27)
        Me.txtBgAdd2.TabIndex = 76
        Me.txtBgAdd2.Text = "0"
        Me.txtBgAdd2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBgSub1
        '
        Me.txtBgSub1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtBgSub1.Location = New System.Drawing.Point(505, 121)
        Me.txtBgSub1.Name = "txtBgSub1"
        Me.txtBgSub1.Size = New System.Drawing.Size(112, 27)
        Me.txtBgSub1.TabIndex = 75
        Me.txtBgSub1.Text = "0"
        Me.txtBgSub1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBgAdd1
        '
        Me.txtBgAdd1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtBgAdd1.Location = New System.Drawing.Point(505, 89)
        Me.txtBgAdd1.Name = "txtBgAdd1"
        Me.txtBgAdd1.Size = New System.Drawing.Size(112, 27)
        Me.txtBgAdd1.TabIndex = 74
        Me.txtBgAdd1.Text = "0"
        Me.txtBgAdd1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.Location = New System.Drawing.Point(241, 193)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(256, 23)
        Me.Label6.TabIndex = 73
        Me.Label6.Text = "                        ""         預算減少數="
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.Location = New System.Drawing.Point(241, 153)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(256, 23)
        Me.Label5.TabIndex = 72
        Me.Label5.Text = "請輸入本年度保管品預算增加數="
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.Location = New System.Drawing.Point(241, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(256, 23)
        Me.Label4.TabIndex = 71
        Me.Label4.Text = "                        ""         預算減少數="
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(241, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(256, 23)
        Me.Label3.TabIndex = 70
        Me.Label3.Text = "請輸入本年度保證金預算增加數="
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(425, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 23)
        Me.Label2.TabIndex = 69
        Me.Label2.Text = "截止日="
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(201, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(232, 23)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "列印雜項負債增減餘額表"
        '
        'BAIL100
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.chkCop)
        Me.Controls.Add(Me.chkEngno_4)
        Me.Controls.Add(Me.dtpEndDate)
        Me.Controls.Add(Me.pBar1)
        Me.Controls.Add(Me.chkAddEngno)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.rdbcourse2)
        Me.Controls.Add(Me.rdbcourse1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnPrt)
        Me.Controls.Add(Me.txtBgSub2)
        Me.Controls.Add(Me.txtBgAdd2)
        Me.Controls.Add(Me.txtBgSub1)
        Me.Controls.Add(Me.txtBgAdd1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "BAIL100"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkCop As System.Windows.Forms.CheckBox
    Friend WithEvents chkEngno_4 As System.Windows.Forms.CheckBox
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents rdbSave As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPrint As System.Windows.Forms.RadioButton
    Friend WithEvents chkAddEngno As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents rdbcourse2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbcourse1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnPrt As System.Windows.Forms.Button
    Friend WithEvents txtBgSub2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBgAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBgSub1 As System.Windows.Forms.TextBox
    Friend WithEvents txtBgAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
