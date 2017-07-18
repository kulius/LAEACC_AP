<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SYSGROUPUNIT
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
        Me.btnall = New System.Windows.Forms.Button()
        Me.btnnoall = New System.Windows.Forms.Button()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblkey = New System.Windows.Forms.Label()
        Me.txtgroup_name = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtgroup_id = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGrid1 = New System.Windows.Forms.DataGridView()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.checked = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.unit_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.unit_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnall
        '
        Me.btnall.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnall.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnall.Location = New System.Drawing.Point(354, 89)
        Me.btnall.Name = "btnall"
        Me.btnall.Size = New System.Drawing.Size(74, 32)
        Me.btnall.TabIndex = 123
        Me.btnall.Text = "全選"
        Me.btnall.UseVisualStyleBackColor = False
        '
        'btnnoall
        '
        Me.btnnoall.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnnoall.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnnoall.Location = New System.Drawing.Point(428, 89)
        Me.btnnoall.Name = "btnnoall"
        Me.btnnoall.Size = New System.Drawing.Size(75, 32)
        Me.btnnoall.TabIndex = 122
        Me.btnnoall.Text = "全取消"
        Me.btnnoall.UseVisualStyleBackColor = False
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.Red
        Me.btnFinish.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnFinish.ForeColor = System.Drawing.Color.White
        Me.btnFinish.Location = New System.Drawing.Point(143, 89)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(91, 36)
        Me.btnFinish.TabIndex = 61
        Me.btnFinish.TabStop = False
        Me.btnFinish.Text = "存檔"
        Me.btnFinish.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.Gold
        Me.btnSearch.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(554, 20)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(155, 36)
        Me.btnSearch.TabIndex = 60
        Me.btnSearch.Text = "載入目前權限"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'lblkey
        '
        Me.lblkey.Location = New System.Drawing.Point(691, 0)
        Me.lblkey.Name = "lblkey"
        Me.lblkey.Size = New System.Drawing.Size(80, 23)
        Me.lblkey.TabIndex = 34
        Me.lblkey.Text = "lblkey"
        '
        'txtgroup_name
        '
        Me.txtgroup_name.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtgroup_name.Location = New System.Drawing.Point(408, 20)
        Me.txtgroup_name.Name = "txtgroup_name"
        Me.txtgroup_name.Size = New System.Drawing.Size(128, 29)
        Me.txtgroup_name.TabIndex = 33
        Me.txtgroup_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label1.Location = New System.Drawing.Point(288, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 24)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "群組名稱"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.BlueViolet
        Me.Label6.Location = New System.Drawing.Point(24, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 24)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "群組代號"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnall)
        Me.Panel1.Controls.Add(Me.btnnoall)
        Me.Panel1.Controls.Add(Me.btnFinish)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.lblkey)
        Me.Panel1.Controls.Add(Me.txtgroup_name)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtgroup_id)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(802, 130)
        Me.Panel1.TabIndex = 0
        '
        'txtgroup_id
        '
        Me.txtgroup_id.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtgroup_id.Location = New System.Drawing.Point(144, 20)
        Me.txtgroup_id.Name = "txtgroup_id"
        Me.txtgroup_id.Size = New System.Drawing.Size(128, 29)
        Me.txtgroup_id.TabIndex = 31
        Me.txtgroup_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.checked, Me.unit_id, Me.unit_name})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(802, 371)
        Me.DataGridView1.TabIndex = 14
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 133)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(802, 371)
        Me.Panel2.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel2)
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(808, 507)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "群組所屬選單"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGrid1
        '
        Me.DataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGrid1.Location = New System.Drawing.Point(3, 3)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.RowTemplate.Height = 24
        Me.DataGrid1.Size = New System.Drawing.Size(802, 501)
        Me.DataGrid1.TabIndex = 13
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGrid1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(808, 507)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "群組"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(816, 540)
        Me.TabControl1.TabIndex = 1
        '
        'checked
        '
        Me.checked.DataPropertyName = "checked"
        Me.checked.FalseValue = "False"
        Me.checked.HeaderText = "是否授權"
        Me.checked.Name = "checked"
        Me.checked.TrueValue = "True"
        '
        'unit_id
        '
        Me.unit_id.DataPropertyName = "unit_id"
        Me.unit_id.HeaderText = "單位代號"
        Me.unit_id.Name = "unit_id"
        Me.unit_id.ReadOnly = True
        '
        'unit_name
        '
        Me.unit_name.DataPropertyName = "unit_name"
        Me.unit_name.HeaderText = "單位名稱"
        Me.unit_name.Name = "unit_name"
        Me.unit_name.ReadOnly = True
        '
        'SYSGROUPUNIT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.ClientSize = New System.Drawing.Size(816, 540)
        Me.Controls.Add(Me.TabControl1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "SYSGROUPUNIT"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnall As System.Windows.Forms.Button
    Friend WithEvents btnnoall As System.Windows.Forms.Button
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblkey As System.Windows.Forms.Label
    Friend WithEvents txtgroup_name As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtgroup_id As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents checked As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents unit_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents unit_name As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
