﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PAY091
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
        Me.rdoPrint = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoPreview = New System.Windows.Forms.RadioButton()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.txtPageNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnSure = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdoPrint
        '
        Me.rdoPrint.Checked = True
        Me.rdoPrint.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPrint.ForeColor = System.Drawing.Color.Green
        Me.rdoPrint.Location = New System.Drawing.Point(16, 24)
        Me.rdoPrint.Name = "rdoPrint"
        Me.rdoPrint.Size = New System.Drawing.Size(96, 24)
        Me.rdoPrint.TabIndex = 2
        Me.rdoPrint.TabStop = True
        Me.rdoPrint.Text = "直接列印"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoPreview)
        Me.GroupBox1.Controls.Add(Me.rdoPrint)
        Me.GroupBox1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(339, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 56)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "列印方式"
        '
        'rdoPreview
        '
        Me.rdoPreview.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdoPreview.ForeColor = System.Drawing.Color.Green
        Me.rdoPreview.Location = New System.Drawing.Point(120, 24)
        Me.rdoPreview.Name = "rdoPreview"
        Me.rdoPreview.Size = New System.Drawing.Size(96, 24)
        Me.rdoPreview.TabIndex = 3
        Me.rdoPreview.Text = "預覽列印"
        '
        'dtpDate
        '
        Me.dtpDate.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(203, 1)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(120, 30)
        Me.dtpDate.TabIndex = 31
        '
        'txtPageNo
        '
        Me.txtPageNo.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtPageNo.Location = New System.Drawing.Point(203, 41)
        Me.txtPageNo.Name = "txtPageNo"
        Me.txtPageNo.Size = New System.Drawing.Size(56, 30)
        Me.txtPageNo.TabIndex = 30
        Me.txtPageNo.Text = "1"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(107, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 23)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "頁次:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Button1.Location = New System.Drawing.Point(715, 25)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 40)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "離開"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnSure
        '
        Me.btnSure.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnSure.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSure.Location = New System.Drawing.Point(611, 25)
        Me.btnSure.Name = "btnSure"
        Me.btnSure.Size = New System.Drawing.Size(64, 40)
        Me.btnSure.TabIndex = 27
        Me.btnSure.Text = "確定"
        Me.btnSure.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(99, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 23)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "收付日期"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "會計科目"
        Me.DataGridTextBoxColumn2.MappingName = "accno"
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "名稱"
        Me.DataGridTextBoxColumn8.MappingName = "bankname"
        Me.DataGridTextBoxColumn8.Width = 200
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "總帳收入"
        Me.DataGridTextBoxColumn9.MappingName = "RAMT"
        Me.DataGridTextBoxColumn9.Width = 75
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "收付日期"
        Me.DataGridTextBoxColumn6.MappingName = "DATE_2"
        Me.DataGridTextBoxColumn6.Width = 85
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "本日共支"
        Me.DataGridTextBoxColumn5.MappingName = "DAY_PAY"
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "本日共收"
        Me.DataGridTextBoxColumn4.MappingName = "DAY_INCOME"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "本日結存"
        Me.DataGridTextBoxColumn3.MappingName = "BALANCE"
        Me.DataGridTextBoxColumn3.Width = 120
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "銀行"
        Me.DataGridTextBoxColumn1.MappingName = "BANK"
        Me.DataGridTextBoxColumn1.Width = 40
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGrid1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn2})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "chf020"
        '
        'DataGrid1
        '
        Me.DataGrid1.CaptionFont = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.DataGrid1.HeaderFont = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(99, 89)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(712, 312)
        Me.DataGrid1.TabIndex = 25
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "總帳支出"
        Me.DataGridTextBoxColumn10.MappingName = "PAMT"
        Me.DataGridTextBoxColumn10.Width = 75
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 589)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(984, 22)
        Me.StatusBar1.TabIndex = 24
        Me.StatusBar1.Text = "StatusBar1"
        '
        'PAY091
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.txtPageNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSure)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGrid1)
        Me.Controls.Add(Me.StatusBar1)
        Me.Name = "PAY091"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdoPrint As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoPreview As System.Windows.Forms.RadioButton
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPageNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnSure As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar

End Class
