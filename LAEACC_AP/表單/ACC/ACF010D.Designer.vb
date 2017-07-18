<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ACF010D
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
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblAmt = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblAccno = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rdbKind1 = New System.Windows.Forms.RadioButton()
        Me.rdbKind2 = New System.Windows.Forms.RadioButton()
        Me.rdbKind3 = New System.Windows.Forms.RadioButton()
        Me.txtNo1 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnGiveUp = New System.Windows.Forms.Button()
        Me.btnSureNo = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(261, 254)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(496, 23)
        Me.lblRemark.TabIndex = 77
        '
        'lblAmt
        '
        Me.lblAmt.ForeColor = System.Drawing.Color.Red
        Me.lblAmt.Location = New System.Drawing.Point(261, 286)
        Me.lblAmt.Name = "lblAmt"
        Me.lblAmt.Size = New System.Drawing.Size(112, 23)
        Me.lblAmt.TabIndex = 76
        Me.lblAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(165, 286)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 23)
        Me.Label4.TabIndex = 75
        Me.Label4.Text = "總帳金額："
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(173, 222)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 24)
        Me.Label9.TabIndex = 71
        Me.Label9.Text = "會計科目:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(112, 24)
        Me.txtYear.MaxLength = 3
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(40, 29)
        Me.txtYear.TabIndex = 67
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(48, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 23)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "年度:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAccno
        '
        Me.lblAccno.Location = New System.Drawing.Point(253, 222)
        Me.lblAccno.Name = "lblAccno"
        Me.lblAccno.Size = New System.Drawing.Size(160, 23)
        Me.lblAccno.TabIndex = 78
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdbKind1)
        Me.GroupBox2.Controls.Add(Me.rdbKind2)
        Me.GroupBox2.Controls.Add(Me.rdbKind3)
        Me.GroupBox2.Location = New System.Drawing.Point(32, 64)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(344, 56)
        Me.GroupBox2.TabIndex = 69
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "傳票種類"
        '
        'rdbKind1
        '
        Me.rdbKind1.ForeColor = System.Drawing.Color.Red
        Me.rdbKind1.Location = New System.Drawing.Point(16, 24)
        Me.rdbKind1.Name = "rdbKind1"
        Me.rdbKind1.Size = New System.Drawing.Size(104, 24)
        Me.rdbKind1.TabIndex = 64
        Me.rdbKind1.Text = "收入傳票"
        '
        'rdbKind2
        '
        Me.rdbKind2.Checked = True
        Me.rdbKind2.ForeColor = System.Drawing.Color.Blue
        Me.rdbKind2.Location = New System.Drawing.Point(120, 24)
        Me.rdbKind2.Name = "rdbKind2"
        Me.rdbKind2.Size = New System.Drawing.Size(104, 24)
        Me.rdbKind2.TabIndex = 65
        Me.rdbKind2.TabStop = True
        Me.rdbKind2.Text = "支出傳票"
        '
        'rdbKind3
        '
        Me.rdbKind3.ForeColor = System.Drawing.Color.Black
        Me.rdbKind3.Location = New System.Drawing.Point(224, 24)
        Me.rdbKind3.Name = "rdbKind3"
        Me.rdbKind3.Size = New System.Drawing.Size(104, 24)
        Me.rdbKind3.TabIndex = 68
        Me.rdbKind3.Text = "轉帳傳票"
        '
        'txtNo1
        '
        Me.txtNo1.Location = New System.Drawing.Point(112, 136)
        Me.txtNo1.MaxLength = 5
        Me.txtNo1.Name = "txtNo1"
        Me.txtNo1.Size = New System.Drawing.Size(80, 29)
        Me.txtNo1.TabIndex = 61
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(24, 136)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 23)
        Me.Label10.TabIndex = 62
        Me.Label10.Text = "製票編號"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Location = New System.Drawing.Point(733, 22)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 32)
        Me.btnExit.TabIndex = 73
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnGiveUp
        '
        Me.btnGiveUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGiveUp.Location = New System.Drawing.Point(541, 302)
        Me.btnGiveUp.Name = "btnGiveUp"
        Me.btnGiveUp.Size = New System.Drawing.Size(75, 32)
        Me.btnGiveUp.TabIndex = 70
        Me.btnGiveUp.Text = "放棄"
        Me.btnGiveUp.UseVisualStyleBackColor = False
        Me.btnGiveUp.Visible = False
        '
        'btnSureNo
        '
        Me.btnSureNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnSureNo.Location = New System.Drawing.Point(216, 136)
        Me.btnSureNo.Name = "btnSureNo"
        Me.btnSureNo.Size = New System.Drawing.Size(88, 32)
        Me.btnSureNo.TabIndex = 3
        Me.btnSureNo.Text = "調出傳票"
        Me.btnSureNo.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtYear)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtNo1)
        Me.GroupBox1.Controls.Add(Me.btnSureNo)
        Me.GroupBox1.Location = New System.Drawing.Point(165, 38)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(552, 176)
        Me.GroupBox1.TabIndex = 74
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "請輸入未處理傳票"
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.Red
        Me.btnFinish.Location = New System.Drawing.Point(445, 302)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(75, 32)
        Me.btnFinish.TabIndex = 69
        Me.btnFinish.Text = "確定"
        Me.btnFinish.UseVisualStyleBackColor = False
        Me.btnFinish.Visible = False
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(173, 254)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 23)
        Me.Label3.TabIndex = 72
        Me.Label3.Text = "摘要："
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ACF010D
        '
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.lblRemark)
        Me.Controls.Add(Me.lblAmt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblAccno)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnGiveUp)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnFinish)
        Me.Controls.Add(Me.Label3)
        Me.Name = "ACF010D"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents lblAmt As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblAccno As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbKind1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbKind2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbKind3 As System.Windows.Forms.RadioButton
    Friend WithEvents txtNo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnGiveUp As System.Windows.Forms.Button
    Friend WithEvents btnSureNo As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
