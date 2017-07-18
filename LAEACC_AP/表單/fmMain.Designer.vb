<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmMain
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fmMain))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.S01 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AC010 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AC030 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AC011 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AC031 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.txtDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtSpace1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtPhone = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtSpace2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtUser = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MenuStrip.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.S01})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(1008, 28)
        Me.MenuStrip.TabIndex = 1
        Me.MenuStrip.Text = "MenuStrip"
        '
        'S01
        '
        Me.S01.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AC010, Me.AC030, Me.AC011, Me.AC031})
        Me.S01.Image = CType(resources.GetObject("S01.Image"), System.Drawing.Image)
        Me.S01.Name = "S01"
        Me.S01.Size = New System.Drawing.Size(101, 24)
        Me.S01.Text = "每日作業"
        '
        'AC010
        '
        Me.AC010.Name = "AC010"
        Me.AC010.Size = New System.Drawing.Size(227, 24)
        Me.AC010.Text = "開立收支傳票 AC010"
        '
        'AC030
        '
        Me.AC030.Name = "AC030"
        Me.AC030.Size = New System.Drawing.Size(227, 24)
        Me.AC030.Text = "開立轉帳傳票 AC030"
        '
        'AC011
        '
        Me.AC011.Name = "AC011"
        Me.AC011.Size = New System.Drawing.Size(227, 24)
        Me.AC011.Text = "修改收支傳票 AC011"
        '
        'AC031
        '
        Me.AC031.Name = "AC031"
        Me.AC031.Size = New System.Drawing.Size(227, 24)
        Me.AC031.Text = "修改轉帳傳票 AC031"
        '
        'Timer
        '
        Me.Timer.Enabled = True
        Me.Timer.Interval = 1000
        '
        'txtDate
        '
        Me.txtDate.Image = CType(resources.GetObject("txtDate.Image"), System.Drawing.Image)
        Me.txtDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(701, 20)
        Me.txtDate.Spring = True
        Me.txtDate.Text = "現在日期及時間"
        '
        'txtSpace1
        '
        Me.txtSpace1.Name = "txtSpace1"
        Me.txtSpace1.Size = New System.Drawing.Size(49, 20)
        Me.txtSpace1.Text = "          "
        '
        'txtPhone
        '
        Me.txtPhone.Image = CType(resources.GetObject("txtPhone.Image"), System.Drawing.Image)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(89, 20)
        Me.txtPhone.Text = "維修專線"
        '
        'txtSpace2
        '
        Me.txtSpace2.Name = "txtSpace2"
        Me.txtSpace2.Size = New System.Drawing.Size(49, 20)
        Me.txtSpace2.Text = "          "
        '
        'txtUser
        '
        Me.txtUser.Image = CType(resources.GetObject("txtUser.Image"), System.Drawing.Image)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(105, 20)
        Me.txtUser.Text = "系統登入者"
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtDate, Me.txtSpace1, Me.txtPhone, Me.txtSpace2, Me.txtUser})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 289)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1008, 25)
        Me.StatusStrip.TabIndex = 2
        Me.StatusStrip.Text = "StatusStrip"
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1008, 261)
        Me.Panel1.TabIndex = 4
        '
        'fmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 314)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "fmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "系統名稱"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents txtDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtSpace1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtPhone As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtSpace2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtUser As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents S01 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents AC010 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AC030 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AC011 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AC031 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
