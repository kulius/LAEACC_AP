﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BAIL060
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
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.rdbIdno = New System.Windows.Forms.RadioButton()
        Me.rdbCop = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtInqCop = New System.Windows.Forms.TextBox()
        Me.btnInq = New System.Windows.Forms.Button()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.btnPrt = New System.Windows.Forms.Button()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.dtgBail = New System.Windows.Forms.DataGrid()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.rdbSafekeep = New System.Windows.Forms.RadioButton()
        Me.rdbBail = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dtgBail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnEnd
        '
        Me.btnEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnEnd.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnEnd.Location = New System.Drawing.Point(814, 32)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(64, 32)
        Me.btnEnd.TabIndex = 65
        Me.btnEnd.Text = "結束"
        Me.btnEnd.UseVisualStyleBackColor = False
        '
        'rdbIdno
        '
        Me.rdbIdno.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbIdno.Location = New System.Drawing.Point(120, 16)
        Me.rdbIdno.Name = "rdbIdno"
        Me.rdbIdno.Size = New System.Drawing.Size(104, 24)
        Me.rdbIdno.TabIndex = 1
        Me.rdbIdno.Text = "商號統編"
        '
        'rdbCop
        '
        Me.rdbCop.Checked = True
        Me.rdbCop.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbCop.Location = New System.Drawing.Point(8, 16)
        Me.rdbCop.Name = "rdbCop"
        Me.rdbCop.Size = New System.Drawing.Size(104, 24)
        Me.rdbCop.TabIndex = 0
        Me.rdbCop.TabStop = True
        Me.rdbCop.Text = "商號名稱"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdbIdno)
        Me.GroupBox2.Controls.Add(Me.rdbCop)
        Me.GroupBox2.Location = New System.Drawing.Point(342, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(232, 48)
        Me.GroupBox2.TabIndex = 63
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "查詢條件"
        '
        'txtInqCop
        '
        Me.txtInqCop.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtInqCop.Location = New System.Drawing.Point(582, 32)
        Me.txtInqCop.MaxLength = 10
        Me.txtInqCop.Name = "txtInqCop"
        Me.txtInqCop.Size = New System.Drawing.Size(88, 27)
        Me.txtInqCop.TabIndex = 62
        '
        'btnInq
        '
        Me.btnInq.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnInq.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnInq.Location = New System.Drawing.Point(686, 32)
        Me.btnInq.Name = "btnInq"
        Me.btnInq.Size = New System.Drawing.Size(64, 32)
        Me.btnInq.TabIndex = 59
        Me.btnInq.Text = "查詢"
        Me.btnInq.UseVisualStyleBackColor = False
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "原因"
        Me.DataGridTextBoxColumn7.MappingName = "reason"
        Me.DataGridTextBoxColumn7.Width = 75
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "隸屬單位"
        Me.DataGridTextBoxColumn6.MappingName = "unit"
        Me.DataGridTextBoxColumn6.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn5.Format = "##,##0"
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "金額"
        Me.DataGridTextBoxColumn5.MappingName = "amt"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 110
        '
        'btnPrt
        '
        Me.btnPrt.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnPrt.Enabled = False
        Me.btnPrt.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnPrt.Location = New System.Drawing.Point(750, 32)
        Me.btnPrt.Name = "btnPrt"
        Me.btnPrt.Size = New System.Drawing.Size(64, 32)
        Me.btnPrt.TabIndex = 64
        Me.btnPrt.Text = "列印"
        Me.btnPrt.UseVisualStyleBackColor = False
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "商號"
        Me.DataGridTextBoxColumn4.MappingName = "cop"
        Me.DataGridTextBoxColumn4.NullText = ""
        Me.DataGridTextBoxColumn4.Width = 90
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "工程名稱"
        Me.DataGridTextBoxColumn2.MappingName = "engname"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 300
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "工程代號"
        Me.DataGridTextBoxColumn1.MappingName = "engno"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 80
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.dtgBail
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.ReadOnly = True
        '
        'dtgBail
        '
        Me.dtgBail.DataMember = ""
        Me.dtgBail.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dtgBail.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dtgBail.Location = New System.Drawing.Point(8, 16)
        Me.dtgBail.Name = "dtgBail"
        Me.dtgBail.PreferredColumnWidth = 80
        Me.dtgBail.Size = New System.Drawing.Size(744, 424)
        Me.dtgBail.TabIndex = 0
        Me.dtgBail.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "到期日"
        Me.DataGridTextBoxColumn3.MappingName = "date_e"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 90
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.dtgBail)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(760, 455)
        Me.TabPage1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(108, 67)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(768, 488)
        Me.TabControl1.TabIndex = 60
        '
        'rdbSafekeep
        '
        Me.rdbSafekeep.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbSafekeep.Location = New System.Drawing.Point(120, 16)
        Me.rdbSafekeep.Name = "rdbSafekeep"
        Me.rdbSafekeep.Size = New System.Drawing.Size(104, 24)
        Me.rdbSafekeep.TabIndex = 1
        Me.rdbSafekeep.Text = "保管品"
        '
        'rdbBail
        '
        Me.rdbBail.Checked = True
        Me.rdbBail.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rdbBail.Location = New System.Drawing.Point(16, 16)
        Me.rdbBail.Name = "rdbBail"
        Me.rdbBail.Size = New System.Drawing.Size(104, 24)
        Me.rdbBail.TabIndex = 0
        Me.rdbBail.TabStop = True
        Me.rdbBail.Text = "保證金"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbSafekeep)
        Me.GroupBox1.Controls.Add(Me.rdbBail)
        Me.GroupBox1.Location = New System.Drawing.Point(104, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(232, 48)
        Me.GroupBox1.TabIndex = 61
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "查詢條件"
        '
        'BAIL060
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtInqCop)
        Me.Controls.Add(Me.btnInq)
        Me.Controls.Add(Me.btnPrt)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "BAIL060"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dtgBail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents rdbIdno As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCop As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtInqCop As System.Windows.Forms.TextBox
    Friend WithEvents btnInq As System.Windows.Forms.Button
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnPrt As System.Windows.Forms.Button
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents dtgBail As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents rdbSafekeep As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBail As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

End Class
