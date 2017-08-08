<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PGMB010_bak
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
        Me.pnlQueryChild = New System.Windows.Forms.Panel()
        Me.btnKindNo2 = New System.Windows.Forms.Button()
        Me.btnKindNo = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboQueryUseyear = New System.Windows.Forms.ComboBox()
        Me.btnQuery = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboQueryMaterial = New System.Windows.Forms.ComboBox()
        Me.txtQueryKindNo2 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtQueryKindNo = New System.Windows.Forms.TextBox()
        Me.txtQueryName = New System.Windows.Forms.TextBox()
        Me.cboQueryUnit = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pnlAddRevise = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkClearDataAfterAdd = New System.Windows.Forms.CheckBox()
        Me.chkViewDataAfterAdd = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnDeleteOK = New System.Windows.Forms.Button()
        Me.btnAddReviseCancel = New System.Windows.Forms.Button()
        Me.btnAddReviseOK = New System.Windows.Forms.Button()
        Me.cboUseYear = New System.Windows.Forms.ComboBox()
        Me.cboMaterial = New System.Windows.Forms.ComboBox()
        Me.cboUnit = New System.Windows.Forms.ComboBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtKindNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dgData = New System.Windows.Forms.DataGrid()
        Me.pnlQueryChild.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.pnlAddRevise.SuspendLayout()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlQueryChild
        '
        Me.pnlQueryChild.AutoScroll = True
        Me.pnlQueryChild.Controls.Add(Me.btnKindNo2)
        Me.pnlQueryChild.Controls.Add(Me.btnKindNo)
        Me.pnlQueryChild.Controls.Add(Me.Label1)
        Me.pnlQueryChild.Controls.Add(Me.cboQueryUseyear)
        Me.pnlQueryChild.Controls.Add(Me.btnQuery)
        Me.pnlQueryChild.Controls.Add(Me.Label5)
        Me.pnlQueryChild.Controls.Add(Me.cboQueryMaterial)
        Me.pnlQueryChild.Controls.Add(Me.txtQueryKindNo2)
        Me.pnlQueryChild.Controls.Add(Me.Label16)
        Me.pnlQueryChild.Controls.Add(Me.Label17)
        Me.pnlQueryChild.Controls.Add(Me.Label18)
        Me.pnlQueryChild.Controls.Add(Me.Label19)
        Me.pnlQueryChild.Controls.Add(Me.txtQueryKindNo)
        Me.pnlQueryChild.Controls.Add(Me.txtQueryName)
        Me.pnlQueryChild.Controls.Add(Me.cboQueryUnit)
        Me.pnlQueryChild.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlQueryChild.Location = New System.Drawing.Point(0, 0)
        Me.pnlQueryChild.Name = "pnlQueryChild"
        Me.pnlQueryChild.Size = New System.Drawing.Size(789, 73)
        Me.pnlQueryChild.TabIndex = 20
        '
        'btnKindNo2
        '
        Me.btnKindNo2.Location = New System.Drawing.Point(182, 12)
        Me.btnKindNo2.Name = "btnKindNo2"
        Me.btnKindNo2.Size = New System.Drawing.Size(20, 19)
        Me.btnKindNo2.TabIndex = 45
        Me.btnKindNo2.Text = "..."
        '
        'btnKindNo
        '
        Me.btnKindNo.Location = New System.Drawing.Point(95, 12)
        Me.btnKindNo.Name = "btnKindNo"
        Me.btnKindNo.Size = New System.Drawing.Size(20, 19)
        Me.btnKindNo.TabIndex = 44
        Me.btnKindNo.Text = "..."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(120, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 17)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "~"
        '
        'cboQueryUseyear
        '
        Me.cboQueryUseyear.Location = New System.Drawing.Point(460, 11)
        Me.cboQueryUseyear.MaxLength = 3
        Me.cboQueryUseyear.Name = "cboQueryUseyear"
        Me.cboQueryUseyear.Size = New System.Drawing.Size(53, 25)
        Me.cboQueryUseyear.TabIndex = 4
        '
        'btnQuery
        '
        Me.btnQuery.Location = New System.Drawing.Point(396, 37)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(116, 26)
        Me.btnQuery.TabIndex = 7
        Me.btnQuery.Text = "查詢"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(393, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 17)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "使用年限"
        '
        'cboQueryMaterial
        '
        Me.cboQueryMaterial.Location = New System.Drawing.Point(260, 37)
        Me.cboQueryMaterial.MaxLength = 10
        Me.cboQueryMaterial.Name = "cboQueryMaterial"
        Me.cboQueryMaterial.Size = New System.Drawing.Size(117, 25)
        Me.cboQueryMaterial.TabIndex = 6
        '
        'txtQueryKindNo2
        '
        Me.txtQueryKindNo2.Location = New System.Drawing.Point(138, 10)
        Me.txtQueryKindNo2.MaxLength = 6
        Me.txtQueryKindNo2.Name = "txtQueryKindNo2"
        Me.txtQueryKindNo2.Size = New System.Drawing.Size(44, 25)
        Me.txtQueryKindNo2.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 12)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(34, 17)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "編號"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(9, 40)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(34, 17)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "名稱"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(220, 12)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(34, 17)
        Me.Label18.TabIndex = 33
        Me.Label18.Text = "單位"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(220, 40)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(34, 17)
        Me.Label19.TabIndex = 34
        Me.Label19.Text = "材質"
        '
        'txtQueryKindNo
        '
        Me.txtQueryKindNo.Location = New System.Drawing.Point(51, 10)
        Me.txtQueryKindNo.MaxLength = 6
        Me.txtQueryKindNo.Name = "txtQueryKindNo"
        Me.txtQueryKindNo.Size = New System.Drawing.Size(43, 25)
        Me.txtQueryKindNo.TabIndex = 1
        '
        'txtQueryName
        '
        Me.txtQueryName.Location = New System.Drawing.Point(51, 38)
        Me.txtQueryName.MaxLength = 30
        Me.txtQueryName.Name = "txtQueryName"
        Me.txtQueryName.Size = New System.Drawing.Size(149, 25)
        Me.txtQueryName.TabIndex = 5
        '
        'cboQueryUnit
        '
        Me.cboQueryUnit.Location = New System.Drawing.Point(260, 11)
        Me.cboQueryUnit.MaxLength = 6
        Me.cboQueryUnit.Name = "cboQueryUnit"
        Me.cboQueryUnit.Size = New System.Drawing.Size(117, 25)
        Me.cboQueryUnit.TabIndex = 3
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 73)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(789, 459)
        Me.TabControl1.TabIndex = 21
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgData)
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(781, 429)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pnlAddRevise)
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(781, 429)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pnlAddRevise
        '
        Me.pnlAddRevise.AutoScroll = True
        Me.pnlAddRevise.Controls.Add(Me.Label2)
        Me.pnlAddRevise.Controls.Add(Me.chkClearDataAfterAdd)
        Me.pnlAddRevise.Controls.Add(Me.chkViewDataAfterAdd)
        Me.pnlAddRevise.Controls.Add(Me.Label12)
        Me.pnlAddRevise.Controls.Add(Me.btnDeleteOK)
        Me.pnlAddRevise.Controls.Add(Me.btnAddReviseCancel)
        Me.pnlAddRevise.Controls.Add(Me.btnAddReviseOK)
        Me.pnlAddRevise.Controls.Add(Me.cboUseYear)
        Me.pnlAddRevise.Controls.Add(Me.cboMaterial)
        Me.pnlAddRevise.Controls.Add(Me.cboUnit)
        Me.pnlAddRevise.Controls.Add(Me.txtName)
        Me.pnlAddRevise.Controls.Add(Me.txtKindNo)
        Me.pnlAddRevise.Controls.Add(Me.Label7)
        Me.pnlAddRevise.Controls.Add(Me.Label8)
        Me.pnlAddRevise.Controls.Add(Me.Label9)
        Me.pnlAddRevise.Controls.Add(Me.Label10)
        Me.pnlAddRevise.Controls.Add(Me.Label11)
        Me.pnlAddRevise.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAddRevise.Location = New System.Drawing.Point(3, 3)
        Me.pnlAddRevise.Name = "pnlAddRevise"
        Me.pnlAddRevise.Size = New System.Drawing.Size(775, 423)
        Me.pnlAddRevise.TabIndex = 21
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(220, 168)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(217, 17)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "注意：使用年限 0 年者代表永久保存"
        '
        'chkClearDataAfterAdd
        '
        Me.chkClearDataAfterAdd.Checked = True
        Me.chkClearDataAfterAdd.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkClearDataAfterAdd.Location = New System.Drawing.Point(13, 113)
        Me.chkClearDataAfterAdd.Name = "chkClearDataAfterAdd"
        Me.chkClearDataAfterAdd.Size = New System.Drawing.Size(296, 20)
        Me.chkClearDataAfterAdd.TabIndex = 56
        Me.chkClearDataAfterAdd.Text = "新增/修改 成功之後清除輸入的資料"
        '
        'chkViewDataAfterAdd
        '
        Me.chkViewDataAfterAdd.Checked = True
        Me.chkViewDataAfterAdd.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkViewDataAfterAdd.Location = New System.Drawing.Point(13, 140)
        Me.chkViewDataAfterAdd.Name = "chkViewDataAfterAdd"
        Me.chkViewDataAfterAdd.Size = New System.Drawing.Size(303, 20)
        Me.chkViewDataAfterAdd.TabIndex = 55
        Me.chkViewDataAfterAdd.Text = "新增/修改/刪除 成功之後立即檢視新資料"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(13, 168)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(202, 17)
        Me.Label12.TabIndex = 54
        Me.Label12.Text = "注意：前面冠有 * 者為必填欄位！"
        '
        'btnDeleteOK
        '
        Me.btnDeleteOK.Location = New System.Drawing.Point(343, 33)
        Me.btnDeleteOK.Name = "btnDeleteOK"
        Me.btnDeleteOK.Size = New System.Drawing.Size(87, 27)
        Me.btnDeleteOK.TabIndex = 14
        Me.btnDeleteOK.Text = "刪除"
        '
        'btnAddReviseCancel
        '
        Me.btnAddReviseCancel.Location = New System.Drawing.Point(343, 60)
        Me.btnAddReviseCancel.Name = "btnAddReviseCancel"
        Me.btnAddReviseCancel.Size = New System.Drawing.Size(87, 27)
        Me.btnAddReviseCancel.TabIndex = 15
        Me.btnAddReviseCancel.Text = "放棄"
        '
        'btnAddReviseOK
        '
        Me.btnAddReviseOK.Location = New System.Drawing.Point(343, 7)
        Me.btnAddReviseOK.Name = "btnAddReviseOK"
        Me.btnAddReviseOK.Size = New System.Drawing.Size(87, 26)
        Me.btnAddReviseOK.TabIndex = 13
        Me.btnAddReviseOK.Text = "確定新增"
        '
        'cboUseYear
        '
        Me.cboUseYear.Location = New System.Drawing.Point(81, 80)
        Me.cboUseYear.MaxLength = 3
        Me.cboUseYear.Name = "cboUseYear"
        Me.cboUseYear.Size = New System.Drawing.Size(76, 25)
        Me.cboUseYear.TabIndex = 12
        '
        'cboMaterial
        '
        Me.cboMaterial.Location = New System.Drawing.Point(217, 47)
        Me.cboMaterial.MaxLength = 10
        Me.cboMaterial.Name = "cboMaterial"
        Me.cboMaterial.Size = New System.Drawing.Size(114, 25)
        Me.cboMaterial.TabIndex = 11
        '
        'cboUnit
        '
        Me.cboUnit.Location = New System.Drawing.Point(81, 47)
        Me.cboUnit.MaxLength = 6
        Me.cboUnit.Name = "cboUnit"
        Me.cboUnit.Size = New System.Drawing.Size(76, 25)
        Me.cboUnit.TabIndex = 10
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(217, 13)
        Me.txtName.MaxLength = 30
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(111, 25)
        Me.txtName.TabIndex = 9
        '
        'txtKindNo
        '
        Me.txtKindNo.Location = New System.Drawing.Point(81, 13)
        Me.txtKindNo.MaxLength = 6
        Me.txtKindNo.Name = "txtKindNo"
        Me.txtKindNo.Size = New System.Drawing.Size(73, 25)
        Me.txtKindNo.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(10, 82)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 17)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "*使用年限"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(174, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(34, 17)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "材質"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 17)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "單位"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(170, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 17)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "*名稱"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(10, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 17)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "*編號"
        '
        'dgData
        '
        Me.dgData.CaptionForeColor = System.Drawing.Color.Blue
        Me.dgData.CaptionText = "財物分類資料"
        Me.dgData.DataMember = ""
        Me.dgData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgData.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgData.Location = New System.Drawing.Point(3, 3)
        Me.dgData.Name = "dgData"
        Me.dgData.Size = New System.Drawing.Size(775, 423)
        Me.dgData.TabIndex = 16
        '
        'PGMB010
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.ClientSize = New System.Drawing.Size(789, 532)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pnlQueryChild)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "PGMB010"
        Me.pnlQueryChild.ResumeLayout(False)
        Me.pnlQueryChild.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.pnlAddRevise.ResumeLayout(False)
        Me.pnlAddRevise.PerformLayout()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlQueryChild As System.Windows.Forms.Panel
    Friend WithEvents btnKindNo2 As System.Windows.Forms.Button
    Friend WithEvents btnKindNo As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboQueryUseyear As System.Windows.Forms.ComboBox
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboQueryMaterial As System.Windows.Forms.ComboBox
    Friend WithEvents txtQueryKindNo2 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtQueryKindNo As System.Windows.Forms.TextBox
    Friend WithEvents txtQueryName As System.Windows.Forms.TextBox
    Friend WithEvents cboQueryUnit As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dgData As System.Windows.Forms.DataGrid
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents pnlAddRevise As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkClearDataAfterAdd As System.Windows.Forms.CheckBox
    Friend WithEvents chkViewDataAfterAdd As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteOK As System.Windows.Forms.Button
    Friend WithEvents btnAddReviseCancel As System.Windows.Forms.Button
    Friend WithEvents btnAddReviseOK As System.Windows.Forms.Button
    Friend WithEvents cboUseYear As System.Windows.Forms.ComboBox
    Friend WithEvents cboMaterial As System.Windows.Forms.ComboBox
    Friend WithEvents cboUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtKindNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label

End Class
