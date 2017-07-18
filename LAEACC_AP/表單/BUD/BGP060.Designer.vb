<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BGP060
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoPreview = New System.Windows.Forms.RadioButton()
        Me.rdoPrint = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.nudMon = New System.Windows.Forms.NumericUpDown()
        Me.nudYear = New System.Windows.Forms.NumericUpDown()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudMon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rdoPreview)
        Me.GroupBox1.Controls.Add(Me.rdoPrint)
        Me.GroupBox1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(295, 258)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 64)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "列印方式"
        '
        'rdoPreview
        '
        Me.rdoPreview.BackColor = System.Drawing.Color.Transparent
        Me.rdoPreview.Checked = True
        Me.rdoPreview.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPreview.ForeColor = System.Drawing.Color.Green
        Me.rdoPreview.Location = New System.Drawing.Point(176, 24)
        Me.rdoPreview.Name = "rdoPreview"
        Me.rdoPreview.Size = New System.Drawing.Size(128, 32)
        Me.rdoPreview.TabIndex = 3
        Me.rdoPreview.TabStop = True
        Me.rdoPreview.Text = "存入excel檔"
        Me.rdoPreview.UseVisualStyleBackColor = False
        '
        'rdoPrint
        '
        Me.rdoPrint.BackColor = System.Drawing.Color.Transparent
        Me.rdoPrint.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPrint.ForeColor = System.Drawing.Color.Green
        Me.rdoPrint.Location = New System.Drawing.Point(48, 24)
        Me.rdoPrint.Name = "rdoPrint"
        Me.rdoPrint.Size = New System.Drawing.Size(112, 32)
        Me.rdoPrint.TabIndex = 2
        Me.rdoPrint.Text = "直接列印"
        Me.rdoPrint.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(351, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(192, 23)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "請輸入列印範圍"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(351, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 23)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "預算執行報告表 BGP060"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnExit.Location = New System.Drawing.Point(487, 354)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(56, 31)
        Me.btnExit.TabIndex = 37
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'BtnPrint
        '
        Me.BtnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnPrint.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(383, 354)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(56, 32)
        Me.BtnPrint.TabIndex = 36
        Me.BtnPrint.Text = "列印"
        Me.BtnPrint.UseVisualStyleBackColor = False
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 597)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(984, 14)
        Me.StatusBar1.TabIndex = 35
        Me.StatusBar1.Text = "StatusBar1"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(359, 202)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 24)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "訖月"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'nudMon
        '
        Me.nudMon.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nudMon.Location = New System.Drawing.Point(447, 194)
        Me.nudMon.Name = "nudMon"
        Me.nudMon.Size = New System.Drawing.Size(56, 30)
        Me.nudMon.TabIndex = 43
        Me.nudMon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudMon.Value = New Decimal(New Integer() {12, 0, 0, 0})
        '
        'nudYear
        '
        Me.nudYear.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nudYear.Location = New System.Drawing.Point(447, 146)
        Me.nudYear.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nudYear.Name = "nudYear"
        Me.nudYear.Size = New System.Drawing.Size(56, 30)
        Me.nudYear.TabIndex = 41
        Me.nudYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudYear.Value = New Decimal(New Integer() {101, 0, 0, 0})
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.Location = New System.Drawing.Point(359, 146)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(80, 24)
        Me.Label15.TabIndex = 42
        Me.Label15.Text = "預算年度"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BGP060
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.nudMon)
        Me.Controls.Add(Me.nudYear)
        Me.Controls.Add(Me.Label15)
        Me.Name = "BGP060"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.nudMon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoPreview As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPrint As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents nudMon As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label15 As System.Windows.Forms.Label

End Class
