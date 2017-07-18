<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AC220
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
        Me.lblChkname = New System.Windows.Forms.Label()
        Me.lblDate_2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNewChkNo = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNewBank = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.txtBank = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rdbKind2 = New System.Windows.Forms.RadioButton()
        Me.rdbKind1 = New System.Windows.Forms.RadioButton()
        Me.gbxNew = New System.Windows.Forms.GroupBox()
        Me.lblAmt = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblNo2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnGiveUp = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtChkNo = New System.Windows.Forms.TextBox()
        Me.btnSureNo = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.gbxNew.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblChkname
        '
        Me.lblChkname.Location = New System.Drawing.Point(112, 96)
        Me.lblChkname.Name = "lblChkname"
        Me.lblChkname.Size = New System.Drawing.Size(496, 23)
        Me.lblChkname.TabIndex = 82
        '
        'lblDate_2
        '
        Me.lblDate_2.ForeColor = System.Drawing.Color.Red
        Me.lblDate_2.Location = New System.Drawing.Point(112, 24)
        Me.lblDate_2.Name = "lblDate_2"
        Me.lblDate_2.Size = New System.Drawing.Size(112, 23)
        Me.lblDate_2.TabIndex = 81
        Me.lblDate_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(8, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 23)
        Me.Label4.TabIndex = 80
        Me.Label4.Text = "受款人："
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewChkNo
        '
        Me.txtNewChkNo.Location = New System.Drawing.Point(352, 56)
        Me.txtNewChkNo.MaxLength = 10
        Me.txtNewChkNo.Name = "txtNewChkNo"
        Me.txtNewChkNo.Size = New System.Drawing.Size(128, 29)
        Me.txtNewChkNo.TabIndex = 78
        '
        'Label13
        '
        Me.Label13.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label13.Location = New System.Drawing.Point(232, 56)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(120, 23)
        Me.Label13.TabIndex = 79
        Me.Label13.Text = "修正支票號："
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewBank
        '
        Me.txtNewBank.Location = New System.Drawing.Point(352, 24)
        Me.txtNewBank.MaxLength = 2
        Me.txtNewBank.Name = "txtNewBank"
        Me.txtNewBank.Size = New System.Drawing.Size(40, 29)
        Me.txtNewBank.TabIndex = 77
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(256, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 23)
        Me.Label12.TabIndex = 76
        Me.Label12.Text = "修正銀行："
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(120, 136)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(496, 23)
        Me.lblRemark.TabIndex = 67
        '
        'txtBank
        '
        Me.txtBank.Location = New System.Drawing.Point(256, 24)
        Me.txtBank.MaxLength = 2
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(40, 29)
        Me.txtBank.TabIndex = 67
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(200, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 24)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "銀行："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rdbKind2
        '
        Me.rdbKind2.Checked = True
        Me.rdbKind2.ForeColor = System.Drawing.Color.Blue
        Me.rdbKind2.Location = New System.Drawing.Point(88, 24)
        Me.rdbKind2.Name = "rdbKind2"
        Me.rdbKind2.Size = New System.Drawing.Size(72, 24)
        Me.rdbKind2.TabIndex = 65
        Me.rdbKind2.TabStop = True
        Me.rdbKind2.Text = "支出"
        '
        'rdbKind1
        '
        Me.rdbKind1.ForeColor = System.Drawing.Color.Red
        Me.rdbKind1.Location = New System.Drawing.Point(16, 24)
        Me.rdbKind1.Name = "rdbKind1"
        Me.rdbKind1.Size = New System.Drawing.Size(72, 24)
        Me.rdbKind1.TabIndex = 64
        Me.rdbKind1.Text = "收入"
        '
        'gbxNew
        '
        Me.gbxNew.Controls.Add(Me.lblChkname)
        Me.gbxNew.Controls.Add(Me.lblAmt)
        Me.gbxNew.Controls.Add(Me.Label3)
        Me.gbxNew.Controls.Add(Me.lblDate_2)
        Me.gbxNew.Controls.Add(Me.Label4)
        Me.gbxNew.Controls.Add(Me.txtNewChkNo)
        Me.gbxNew.Controls.Add(Me.Label13)
        Me.gbxNew.Controls.Add(Me.txtNewBank)
        Me.gbxNew.Controls.Add(Me.Label12)
        Me.gbxNew.Controls.Add(Me.lblRemark)
        Me.gbxNew.Controls.Add(Me.lblNo2)
        Me.gbxNew.Controls.Add(Me.Label5)
        Me.gbxNew.Controls.Add(Me.btnGiveUp)
        Me.gbxNew.Controls.Add(Me.Label9)
        Me.gbxNew.Controls.Add(Me.Label11)
        Me.gbxNew.Controls.Add(Me.btnFinish)
        Me.gbxNew.Enabled = False
        Me.gbxNew.Location = New System.Drawing.Point(189, 123)
        Me.gbxNew.Name = "gbxNew"
        Me.gbxNew.Size = New System.Drawing.Size(616, 256)
        Me.gbxNew.TabIndex = 85
        Me.gbxNew.TabStop = False
        '
        'lblAmt
        '
        Me.lblAmt.ForeColor = System.Drawing.Color.Red
        Me.lblAmt.Location = New System.Drawing.Point(112, 56)
        Me.lblAmt.Name = "lblAmt"
        Me.lblAmt.Size = New System.Drawing.Size(112, 23)
        Me.lblAmt.TabIndex = 43
        Me.lblAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(32, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 23)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "摘要："
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNo2
        '
        Me.lblNo2.Location = New System.Drawing.Point(120, 176)
        Me.lblNo2.Name = "lblNo2"
        Me.lblNo2.Size = New System.Drawing.Size(288, 23)
        Me.lblNo2.TabIndex = 59
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(0, 170)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 23)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "傳票起訖號："
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnGiveUp
        '
        Me.btnGiveUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGiveUp.Location = New System.Drawing.Point(472, 208)
        Me.btnGiveUp.Name = "btnGiveUp"
        Me.btnGiveUp.Size = New System.Drawing.Size(75, 32)
        Me.btnGiveUp.TabIndex = 8
        Me.btnGiveUp.Text = "放棄"
        Me.btnGiveUp.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(16, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 24)
        Me.Label9.TabIndex = 36
        Me.Label9.Text = "收付款日："
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(24, 61)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 24)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "支票金額："
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.Red
        Me.btnFinish.Enabled = False
        Me.btnFinish.Location = New System.Drawing.Point(376, 208)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(75, 32)
        Me.btnFinish.TabIndex = 7
        Me.btnFinish.Text = "確定"
        Me.btnFinish.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtBank)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.rdbKind2)
        Me.GroupBox1.Controls.Add(Me.rdbKind1)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtChkNo)
        Me.GroupBox1.Controls.Add(Me.btnSureNo)
        Me.GroupBox1.Location = New System.Drawing.Point(189, 51)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(616, 64)
        Me.GroupBox1.TabIndex = 84
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "請輸入原支票"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(312, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 23)
        Me.Label10.TabIndex = 62
        Me.Label10.Text = "支票號"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChkNo
        '
        Me.txtChkNo.Location = New System.Drawing.Point(368, 24)
        Me.txtChkNo.MaxLength = 10
        Me.txtChkNo.Name = "txtChkNo"
        Me.txtChkNo.Size = New System.Drawing.Size(128, 29)
        Me.txtChkNo.TabIndex = 61
        '
        'btnSureNo
        '
        Me.btnSureNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnSureNo.Location = New System.Drawing.Point(520, 24)
        Me.btnSureNo.Name = "btnSureNo"
        Me.btnSureNo.Size = New System.Drawing.Size(88, 32)
        Me.btnSureNo.TabIndex = 3
        Me.btnSureNo.Text = "調出支票"
        Me.btnSureNo.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Location = New System.Drawing.Point(749, 11)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(56, 32)
        Me.btnExit.TabIndex = 83
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'AC220
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.gbxNew)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "AC220"
        Me.gbxNew.ResumeLayout(False)
        Me.gbxNew.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblChkname As System.Windows.Forms.Label
    Friend WithEvents lblDate_2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNewChkNo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtNewBank As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rdbKind2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbKind1 As System.Windows.Forms.RadioButton
    Friend WithEvents gbxNew As System.Windows.Forms.GroupBox
    Friend WithEvents lblAmt As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblNo2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnGiveUp As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtChkNo As System.Windows.Forms.TextBox
    Friend WithEvents btnSureNo As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button

End Class
