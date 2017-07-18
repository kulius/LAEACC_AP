Public NotInheritable Class fmAbout
#Region "@Form及功能操作@"
    Private Sub fmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '-- 初始化 --
        Me.Text = "關於 " & INI_Read("CONFIG", "SYSTEM", "NAME") '系統名稱
        txtProductName.Text = INI_Read("CONFIG", "SYSTEM", "NAME") '產品名稱
        txtVersion.Text = "版本 " & INI_Read("CONFIG", "SYSTEM", "VAR") '版本
        txtCopyright.Text = "Copyright © 2014 " & INI_Read("CONFIG", "BASIC", "NAME") '著作權
        txtCompanyName.Text = INI_Read("CONFIG", "BASIC", "NAME") '公司名稱
        txtDescription.Text = INI_Read("CONFIG", "BASIC", "DESCRIPTION") '描述
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub
#End Region
End Class
