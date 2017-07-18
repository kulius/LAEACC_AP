<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PAY023
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblNo1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblChkname = New System.Windows.Forms.Label()
        Me.lblBankname = New System.Windows.Forms.Label()
        Me.btnGiveUp = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNewChkno = New System.Windows.Forms.TextBox()
        Me.txtBank = New System.Windows.Forms.TextBox()
        Me.txtChkname = New System.Windows.Forms.TextBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.txtChkNo = New System.Windows.Forms.TextBox()
        Me.btnChkno = New System.Windows.Forms.Button()
        Me.lblamt = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboName = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(377, 257)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(352, 24)
        Me.Label6.TabIndex = 85
        Me.Label6.Text = "一般支票欲轉電子支票者,請輸入TRyyy00000"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExit.Location = New System.Drawing.Point(753, 9)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 32)
        Me.btnExit.TabIndex = 84
        Me.btnExit.Text = "結束"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblNo1
        '
        Me.lblNo1.Location = New System.Drawing.Point(257, 153)
        Me.lblNo1.Name = "lblNo1"
        Me.lblNo1.Size = New System.Drawing.Size(488, 23)
        Me.lblNo1.TabIndex = 83
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(169, 153)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 23)
        Me.Label5.TabIndex = 82
        Me.Label5.Text = "傳票："
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMsg
        '
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(489, 73)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(160, 23)
        Me.lblMsg.TabIndex = 81
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(249, 217)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(344, 23)
        Me.lblRemark.TabIndex = 80
        Me.lblRemark.Visible = False
        '
        'lblChkname
        '
        Me.lblChkname.Location = New System.Drawing.Point(257, 185)
        Me.lblChkname.Name = "lblChkname"
        Me.lblChkname.Size = New System.Drawing.Size(344, 23)
        Me.lblChkname.TabIndex = 79
        Me.lblChkname.Visible = False
        '
        'lblBankname
        '
        Me.lblBankname.Location = New System.Drawing.Point(329, 33)
        Me.lblBankname.Name = "lblBankname"
        Me.lblBankname.Size = New System.Drawing.Size(328, 23)
        Me.lblBankname.TabIndex = 78
        '
        'btnGiveUp
        '
        Me.btnGiveUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGiveUp.Location = New System.Drawing.Point(473, 401)
        Me.btnGiveUp.Name = "btnGiveUp"
        Me.btnGiveUp.Size = New System.Drawing.Size(75, 32)
        Me.btnGiveUp.TabIndex = 69
        Me.btnGiveUp.Text = "放棄"
        Me.btnGiveUp.UseVisualStyleBackColor = False
        Me.btnGiveUp.Visible = False
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(137, 249)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 24)
        Me.Label4.TabIndex = 77
        Me.Label4.Text = "新支票號碼："
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewChkno
        '
        Me.txtNewChkno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNewChkno.Location = New System.Drawing.Point(257, 249)
        Me.txtNewChkno.MaxLength = 10
        Me.txtNewChkno.Name = "txtNewChkno"
        Me.txtNewChkno.Size = New System.Drawing.Size(112, 29)
        Me.txtNewChkno.TabIndex = 65
        Me.txtNewChkno.Visible = False
        '
        'txtBank
        '
        Me.txtBank.Location = New System.Drawing.Point(257, 33)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(56, 29)
        Me.txtBank.TabIndex = 62
        '
        'txtChkname
        '
        Me.txtChkname.Location = New System.Drawing.Point(257, 297)
        Me.txtChkname.MaxLength = 50
        Me.txtChkname.Name = "txtChkname"
        Me.txtChkname.Size = New System.Drawing.Size(488, 29)
        Me.txtChkname.TabIndex = 66
        Me.txtChkname.Visible = False
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(257, 345)
        Me.txtRemark.MaxLength = 60
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(584, 29)
        Me.txtRemark.TabIndex = 67
        Me.txtRemark.Visible = False
        '
        'txtChkNo
        '
        Me.txtChkNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtChkNo.Location = New System.Drawing.Point(257, 73)
        Me.txtChkNo.MaxLength = 10
        Me.txtChkNo.Name = "txtChkNo"
        Me.txtChkNo.Size = New System.Drawing.Size(112, 29)
        Me.txtChkNo.TabIndex = 63
        '
        'btnChkno
        '
        Me.btnChkno.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnChkno.Location = New System.Drawing.Point(377, 65)
        Me.btnChkno.Name = "btnChkno"
        Me.btnChkno.Size = New System.Drawing.Size(96, 32)
        Me.btnChkno.TabIndex = 64
        Me.btnChkno.Text = "調出支票"
        Me.btnChkno.UseVisualStyleBackColor = False
        '
        'lblamt
        '
        Me.lblamt.ForeColor = System.Drawing.Color.Red
        Me.lblamt.Location = New System.Drawing.Point(257, 121)
        Me.lblamt.Name = "lblamt"
        Me.lblamt.Size = New System.Drawing.Size(112, 23)
        Me.lblamt.TabIndex = 74
        Me.lblamt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(161, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 23)
        Me.Label2.TabIndex = 73
        Me.Label2.Text = "支票金額："
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.Red
        Me.btnFinish.Location = New System.Drawing.Point(321, 401)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(75, 32)
        Me.btnFinish.TabIndex = 68
        Me.btnFinish.Text = "確定"
        Me.btnFinish.UseVisualStyleBackColor = False
        Me.btnFinish.Visible = False
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(161, 73)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 24)
        Me.Label11.TabIndex = 72
        Me.Label11.Text = "支票號碼："
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(177, 33)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 24)
        Me.Label9.TabIndex = 71
        Me.Label9.Text = "銀行："
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(161, 345)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 23)
        Me.Label3.TabIndex = 76
        Me.Label3.Text = "摘要："
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(169, 297)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 23)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "受款人："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboName
        '
        Me.cboName.Location = New System.Drawing.Point(257, 297)
        Me.cboName.MaxDropDownItems = 20
        Me.cboName.Name = "cboName"
        Me.cboName.Size = New System.Drawing.Size(504, 28)
        Me.cboName.Sorted = True
        Me.cboName.TabIndex = 70
        Me.cboName.Visible = False
        '
        'PAY023
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblNo1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.lblRemark)
        Me.Controls.Add(Me.lblChkname)
        Me.Controls.Add(Me.lblBankname)
        Me.Controls.Add(Me.btnGiveUp)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtNewChkno)
        Me.Controls.Add(Me.txtBank)
        Me.Controls.Add(Me.txtChkname)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.txtChkNo)
        Me.Controls.Add(Me.btnChkno)
        Me.Controls.Add(Me.lblamt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnFinish)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboName)
        Me.Name = "PAY023"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblNo1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents lblChkname As System.Windows.Forms.Label
    Friend WithEvents lblBankname As System.Windows.Forms.Label
    Friend WithEvents btnGiveUp As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNewChkno As System.Windows.Forms.TextBox
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents txtChkname As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents txtChkNo As System.Windows.Forms.TextBox
    Friend WithEvents btnChkno As System.Windows.Forms.Button
    Friend WithEvents lblamt As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboName As System.Windows.Forms.ComboBox

End Class
