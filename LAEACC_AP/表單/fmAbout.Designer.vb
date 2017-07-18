<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmAbout
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

    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents txtProductName As System.Windows.Forms.Label
    Friend WithEvents txtVersion As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents txtCopyright As System.Windows.Forms.Label

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fmAbout))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox()
        Me.txtProductName = New System.Windows.Forms.Label()
        Me.txtVersion = New System.Windows.Forms.Label()
        Me.txtCopyright = New System.Windows.Forms.Label()
        Me.txtCompanyName = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 2
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.0!))
        Me.TableLayoutPanel.Controls.Add(Me.LogoPictureBox, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.txtProductName, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.txtVersion, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.txtCopyright, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.txtCompanyName, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.txtDescription, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.OKButton, 1, 5)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(9, 8)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 6
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(396, 239)
        Me.TableLayoutPanel.TabIndex = 0
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), System.Drawing.Image)
        Me.LogoPictureBox.Location = New System.Drawing.Point(3, 3)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.TableLayoutPanel.SetRowSpan(Me.LogoPictureBox, 6)
        Me.LogoPictureBox.Size = New System.Drawing.Size(124, 233)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoPictureBox.TabIndex = 0
        Me.LogoPictureBox.TabStop = False
        '
        'txtProductName
        '
        Me.txtProductName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtProductName.Location = New System.Drawing.Point(136, 0)
        Me.txtProductName.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.txtProductName.MaximumSize = New System.Drawing.Size(0, 16)
        Me.txtProductName.Name = "txtProductName"
        Me.txtProductName.Size = New System.Drawing.Size(257, 16)
        Me.txtProductName.TabIndex = 0
        Me.txtProductName.Text = "產品名稱"
        Me.txtProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVersion
        '
        Me.txtVersion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtVersion.Location = New System.Drawing.Point(136, 23)
        Me.txtVersion.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.txtVersion.MaximumSize = New System.Drawing.Size(0, 16)
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.Size = New System.Drawing.Size(257, 16)
        Me.txtVersion.TabIndex = 0
        Me.txtVersion.Text = "版本"
        Me.txtVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCopyright
        '
        Me.txtCopyright.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCopyright.Location = New System.Drawing.Point(136, 46)
        Me.txtCopyright.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.txtCopyright.MaximumSize = New System.Drawing.Size(0, 16)
        Me.txtCopyright.Name = "txtCopyright"
        Me.txtCopyright.Size = New System.Drawing.Size(257, 16)
        Me.txtCopyright.TabIndex = 0
        Me.txtCopyright.Text = "著作權"
        Me.txtCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCompanyName.Location = New System.Drawing.Point(136, 69)
        Me.txtCompanyName.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.txtCompanyName.MaximumSize = New System.Drawing.Size(0, 16)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(257, 16)
        Me.txtCompanyName.TabIndex = 0
        Me.txtCompanyName.Text = "公司名稱"
        Me.txtCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescription
        '
        Me.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDescription.Location = New System.Drawing.Point(136, 95)
        Me.txtDescription.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDescription.Size = New System.Drawing.Size(257, 113)
        Me.txtDescription.TabIndex = 0
        Me.txtDescription.TabStop = False
        Me.txtDescription.Text = "描述 :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(在執行階段中，將會以應用程式的組件資訊取代標籤的文字。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "請在專案設計工具的 [應用程式] 窗格中，自訂應用程式的組件資訊。)"
        '
        'OKButton
        '
        Me.OKButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.OKButton.Location = New System.Drawing.Point(318, 215)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(75, 21)
        Me.OKButton.TabIndex = 0
        Me.OKButton.Text = "確定(&O)"
        '
        'fmAbout
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.OKButton
        Me.ClientSize = New System.Drawing.Size(414, 255)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fmAbout"
        Me.Padding = New System.Windows.Forms.Padding(9, 8, 9, 8)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "系統名稱"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

End Class
