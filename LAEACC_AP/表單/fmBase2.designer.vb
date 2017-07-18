<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmBase2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fmBase2))
        Me.BCtrl = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.butExit = New System.Windows.Forms.ToolStripButton()
        Me.Separator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.butFirst = New System.Windows.Forms.ToolStripButton()
        Me.butPrior = New System.Windows.Forms.ToolStripButton()
        Me.butNext = New System.Windows.Forms.ToolStripButton()
        Me.butLast = New System.Windows.Forms.ToolStripButton()
        Me.Separator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.butSave = New System.Windows.Forms.ToolStripButton()
        Me.butCancelEdit = New System.Windows.Forms.ToolStripButton()
        Me.butAddNew = New System.Windows.Forms.ToolStripButton()
        Me.butEdit = New System.Windows.Forms.ToolStripButton()
        Me.butDelete = New System.Windows.Forms.ToolStripButton()
        Me.Separator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.butFind = New System.Windows.Forms.ToolStripButton()
        Me.butSearch = New System.Windows.Forms.ToolStripButton()
        Me.butPrint = New System.Windows.Forms.ToolStripButton()
        Me.BaseStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.txtItemStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtSeat = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.butCopy = New System.Windows.Forms.ToolStripButton()
        CType(Me.BCtrl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BCtrl.SuspendLayout()
        Me.BaseStatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'BCtrl
        '
        Me.BCtrl.AddNewItem = Nothing
        Me.BCtrl.CountItem = Nothing
        Me.BCtrl.DeleteItem = Nothing
        Me.BCtrl.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.butExit, Me.Separator1, Me.butFirst, Me.butPrior, Me.butNext, Me.butLast, Me.Separator2, Me.butSave, Me.butCancelEdit, Me.butAddNew, Me.butCopy, Me.butEdit, Me.butDelete, Me.Separator3, Me.butFind, Me.butSearch, Me.butPrint})
        Me.BCtrl.Location = New System.Drawing.Point(0, 0)
        Me.BCtrl.MoveFirstItem = Nothing
        Me.BCtrl.MoveLastItem = Nothing
        Me.BCtrl.MoveNextItem = Nothing
        Me.BCtrl.MovePreviousItem = Nothing
        Me.BCtrl.Name = "BCtrl"
        Me.BCtrl.PositionItem = Nothing
        Me.BCtrl.Size = New System.Drawing.Size(1016, 43)
        Me.BCtrl.TabIndex = 1
        Me.BCtrl.Text = "BaseBindingNavigator"
        '
        'butExit
        '
        Me.butExit.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butExit.Image = CType(resources.GetObject("butExit.Image"), System.Drawing.Image)
        Me.butExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butExit.Name = "butExit"
        Me.butExit.Size = New System.Drawing.Size(45, 40)
        Me.butExit.Text = "離開"
        Me.butExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Separator1
        '
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(6, 43)
        '
        'butFirst
        '
        Me.butFirst.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butFirst.Image = CType(resources.GetObject("butFirst.Image"), System.Drawing.Image)
        Me.butFirst.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butFirst.Name = "butFirst"
        Me.butFirst.Size = New System.Drawing.Size(61, 40)
        Me.butFirst.Text = "第一筆"
        Me.butFirst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'butPrior
        '
        Me.butPrior.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butPrior.Image = CType(resources.GetObject("butPrior.Image"), System.Drawing.Image)
        Me.butPrior.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butPrior.Name = "butPrior"
        Me.butPrior.Size = New System.Drawing.Size(61, 40)
        Me.butPrior.Text = "上一筆"
        Me.butPrior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'butNext
        '
        Me.butNext.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butNext.Image = CType(resources.GetObject("butNext.Image"), System.Drawing.Image)
        Me.butNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butNext.Name = "butNext"
        Me.butNext.Size = New System.Drawing.Size(61, 40)
        Me.butNext.Text = "下一筆"
        Me.butNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'butLast
        '
        Me.butLast.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butLast.Image = CType(resources.GetObject("butLast.Image"), System.Drawing.Image)
        Me.butLast.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butLast.Name = "butLast"
        Me.butLast.Size = New System.Drawing.Size(61, 40)
        Me.butLast.Text = "最末筆"
        Me.butLast.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Separator2
        '
        Me.Separator2.Name = "Separator2"
        Me.Separator2.Size = New System.Drawing.Size(6, 43)
        '
        'butSave
        '
        Me.butSave.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butSave.Image = CType(resources.GetObject("butSave.Image"), System.Drawing.Image)
        Me.butSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butSave.Name = "butSave"
        Me.butSave.Size = New System.Drawing.Size(45, 40)
        Me.butSave.Text = "儲存"
        Me.butSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'butCancelEdit
        '
        Me.butCancelEdit.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butCancelEdit.Image = CType(resources.GetObject("butCancelEdit.Image"), System.Drawing.Image)
        Me.butCancelEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butCancelEdit.Name = "butCancelEdit"
        Me.butCancelEdit.Size = New System.Drawing.Size(45, 40)
        Me.butCancelEdit.Text = "還原"
        Me.butCancelEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'butAddNew
        '
        Me.butAddNew.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butAddNew.Image = CType(resources.GetObject("butAddNew.Image"), System.Drawing.Image)
        Me.butAddNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butAddNew.Name = "butAddNew"
        Me.butAddNew.Size = New System.Drawing.Size(45, 40)
        Me.butAddNew.Text = "新增"
        Me.butAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'butEdit
        '
        Me.butEdit.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butEdit.Image = CType(resources.GetObject("butEdit.Image"), System.Drawing.Image)
        Me.butEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butEdit.Name = "butEdit"
        Me.butEdit.Size = New System.Drawing.Size(45, 40)
        Me.butEdit.Text = "修改"
        Me.butEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'butDelete
        '
        Me.butDelete.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butDelete.Image = CType(resources.GetObject("butDelete.Image"), System.Drawing.Image)
        Me.butDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butDelete.Name = "butDelete"
        Me.butDelete.Size = New System.Drawing.Size(45, 40)
        Me.butDelete.Text = "刪除"
        Me.butDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Separator3
        '
        Me.Separator3.Name = "Separator3"
        Me.Separator3.Size = New System.Drawing.Size(6, 43)
        '
        'butFind
        '
        Me.butFind.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butFind.Image = CType(resources.GetObject("butFind.Image"), System.Drawing.Image)
        Me.butFind.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butFind.Name = "butFind"
        Me.butFind.Size = New System.Drawing.Size(45, 40)
        Me.butFind.Text = "尋找"
        Me.butFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'butSearch
        '
        Me.butSearch.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butSearch.Image = CType(resources.GetObject("butSearch.Image"), System.Drawing.Image)
        Me.butSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butSearch.Name = "butSearch"
        Me.butSearch.Size = New System.Drawing.Size(45, 40)
        Me.butSearch.Text = "查詢"
        Me.butSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'butPrint
        '
        Me.butPrint.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butPrint.Image = CType(resources.GetObject("butPrint.Image"), System.Drawing.Image)
        Me.butPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butPrint.Name = "butPrint"
        Me.butPrint.Size = New System.Drawing.Size(45, 40)
        Me.butPrint.Text = "列印"
        Me.butPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'BaseStatusStrip
        '
        Me.BaseStatusStrip.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BaseStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtItemStatus, Me.ToolStripStatusLabel1, Me.txtSeat, Me.ToolStripStatusLabel3, Me.txtCount})
        Me.BaseStatusStrip.Location = New System.Drawing.Point(0, 716)
        Me.BaseStatusStrip.Name = "BaseStatusStrip"
        Me.BaseStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.BaseStatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BaseStatusStrip.ShowItemToolTips = True
        Me.BaseStatusStrip.Size = New System.Drawing.Size(1016, 25)
        Me.BaseStatusStrip.TabIndex = 2
        Me.BaseStatusStrip.Text = "BaseStatusStrip"
        '
        'txtItemStatus
        '
        Me.txtItemStatus.Name = "txtItemStatus"
        Me.txtItemStatus.Size = New System.Drawing.Size(787, 20)
        Me.txtItemStatus.Spring = True
        Me.txtItemStatus.Text = "操作狀態"
        Me.txtItemStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(89, 20)
        Me.ToolStripStatusLabel1.Text = "目前位置："
        '
        'txtSeat
        '
        Me.txtSeat.Name = "txtSeat"
        Me.txtSeat.Size = New System.Drawing.Size(18, 20)
        Me.txtSeat.Text = "1"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(89, 20)
        Me.ToolStripStatusLabel3.Text = "資料筆數："
        '
        'txtCount
        '
        Me.txtCount.Name = "txtCount"
        Me.txtCount.Size = New System.Drawing.Size(18, 20)
        Me.txtCount.Text = "1"
        '
        'butCopy
        '
        Me.butCopy.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butCopy.Image = CType(resources.GetObject("butCopy.Image"), System.Drawing.Image)
        Me.butCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butCopy.Name = "butCopy"
        Me.butCopy.Size = New System.Drawing.Size(45, 40)
        Me.butCopy.Text = "複製"
        Me.butCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'fmBase2
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(1016, 741)
        Me.Controls.Add(Me.BaseStatusStrip)
        Me.Controls.Add(Me.BCtrl)
        Me.Font = New System.Drawing.Font("微軟正黑體", 12.0!)
        Me.Name = "fmBase2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "fmBase2"
        CType(Me.BCtrl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BCtrl.ResumeLayout(False)
        Me.BCtrl.PerformLayout()
        Me.BaseStatusStrip.ResumeLayout(False)
        Me.BaseStatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BCtrl As System.Windows.Forms.BindingNavigator
    Friend WithEvents butExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents Separator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents butFirst As System.Windows.Forms.ToolStripButton
    Friend WithEvents butPrior As System.Windows.Forms.ToolStripButton
    Friend WithEvents butNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents butLast As System.Windows.Forms.ToolStripButton
    Friend WithEvents Separator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents butSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents butCancelEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents butAddNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents butEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents butDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents Separator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents butFind As System.Windows.Forms.ToolStripButton
    Friend WithEvents butPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents BaseStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents txtItemStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtSeat As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents butSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents butCopy As System.Windows.Forms.ToolStripButton
End Class
