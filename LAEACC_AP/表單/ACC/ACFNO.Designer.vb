<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ACFNO
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
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.lblkey2 = New System.Windows.Forms.Label()
        Me.lblkey1 = New System.Windows.Forms.Label()
        Me.RecMove1 = New LAEACC_AP.RecMove()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.DataGrid1.HeaderFont = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(74, 23)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(384, 368)
        Me.DataGrid1.TabIndex = 6
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.DataGrid1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "acfno"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "年度"
        Me.DataGridTextBoxColumn1.MappingName = "accyear"
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "種類"
        Me.DataGridTextBoxColumn2.MappingName = "kind"
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn3.Format = "###,###"
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "目前控制號數"
        Me.DataGridTextBoxColumn3.MappingName = "cont_no"
        Me.DataGridTextBoxColumn3.Width = 120
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ListBox1.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ListBox1.Items.AddRange(New Object() {"種類      控制項目", "1.收入傳票製票編號", "2.支出傳票製票編號", "3.轉帳傳票製票編號", "4.收入傳票製票編號", "5.支出傳票製票編號", "6.轉帳傳票製票編號", "7.出納現金結存表頁次控制", "B.預算請購編號控制", "D.會計日計表列印頁次控制 "})
        Me.ListBox1.Location = New System.Drawing.Point(482, 55)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(208, 134)
        Me.ListBox1.TabIndex = 9
        '
        'lblkey2
        '
        Me.lblkey2.Location = New System.Drawing.Point(552, 404)
        Me.lblkey2.Name = "lblkey2"
        Me.lblkey2.Size = New System.Drawing.Size(40, 23)
        Me.lblkey2.TabIndex = 8
        Me.lblkey2.Text = "lblkey2"
        '
        'lblkey1
        '
        Me.lblkey1.Location = New System.Drawing.Point(512, 404)
        Me.lblkey1.Name = "lblkey1"
        Me.lblkey1.Size = New System.Drawing.Size(40, 23)
        Me.lblkey1.TabIndex = 7
        Me.lblkey1.Text = "lblkey1"
        '
        'RecMove1
        '
        Me.RecMove1.FilesPara = Nothing
        Me.RecMove1.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.RecMove1.GenInsFunc = "() values ()"
        Me.RecMove1.genupdfunc = ""
        Me.RecMove1.Location = New System.Drawing.Point(43, 397)
        Me.RecMove1.Name = "RecMove1"
        Me.RecMove1.Setds = Nothing
        Me.RecMove1.Setpos = 0
        Me.RecMove1.Size = New System.Drawing.Size(463, 41)
        Me.RecMove1.TabIndex = 10
        '
        'ACFNO
        '
        Me.ClientSize = New System.Drawing.Size(856, 611)
        Me.Controls.Add(Me.RecMove1)
        Me.Controls.Add(Me.DataGrid1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.lblkey2)
        Me.Controls.Add(Me.lblkey1)
        Me.Name = "ACFNO"
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Protected WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents lblkey2 As System.Windows.Forms.Label
    Friend WithEvents lblkey1 As System.Windows.Forms.Label
    Friend WithEvents RecMove1 As LAEACC_AP.RecMove

End Class
