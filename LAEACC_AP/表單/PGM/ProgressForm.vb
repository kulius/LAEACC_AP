
Imports System.Drawing
Public Class ProgressForm
    Inherits System.Windows.Forms.Form
    Private Sub ProgressForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub HideMe()
        Me.Hide()
    End Sub

    Public Sub ShowMessage(ByVal message As String)
        lblMsg.Text = message
        lblMsg.Location = New Point((Me.ClientSize.Width - lblMsg.Width) / 2, (Me.ClientSize.Height - lblMsg.Height) / 2)
        Me.Show()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Public Overloads Sub ShowQuery()
        ShowMessage("資料查詢中，請稍候...")
    End Sub

    Public Overloads Sub ShowDelete()
        ShowMessage("資料刪除中，請稍候...")
    End Sub

    Public Overloads Sub ShowUpdate()
        ShowMessage("資料修改中，請稍候...")
    End Sub

    Public Overloads Sub ShowInsert()
        ShowMessage("資料新增中，請稍候...")
    End Sub

    Public Overloads Sub ShowDoing()
        ShowMessage("程式執行中，請稍候...")
    End Sub
End Class