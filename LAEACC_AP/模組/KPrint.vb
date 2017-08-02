Imports CHIA.Utility
Imports JBC.Printing
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.Win32
Imports System
Imports System.Collections
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Configuration
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms
Imports System.Drawing.Size

Public Class KPrint
    Inherits JBC.Printing.FPPrinter
    Public Sub New()
        MyBase.New()

        Me.PrintPreviewDialog.Text = ("預覽列印-測試中")

        Dim control As Control
        For Each control In Me.PrintPreviewDialog.Controls
            If (control.GetType Is GetType(ToolStrip)) Then
                Dim button As ToolStripButton
                Dim bar As ToolStrip = DirectCast(control, ToolStrip)
   
                Dim button3 As New ToolStripButton
                Dim button7 As ToolStripButton = button3
                button7.Text = "設定印表機"
                button7.Width = 100
                button7.Height = 20
                button7 = Nothing

                Dim button2 As New ToolStripButton
                Dim button6 As ToolStripButton = button2
                button6.Text = "修改邊界"
                button6.Width = 200
                button6.Height = 20
                button6 = Nothing

                AddHandler button3.Click, New EventHandler(AddressOf Me.PrintInPreviewWindow)
                AddHandler button2.Click, New EventHandler(AddressOf Me.ReviseMargin)

                bar.Items.Add(button3)
                bar.Items.Add(button2)


                Me.PrintPreviewDialog.CancelButton = button
                Continue For
            End If
            If (control.GetType Is GetType(PrintPreviewControl)) Then
                Me.PrintPreviewControl = DirectCast(control, PrintPreviewControl)
            End If
        Next

    End Sub



    Private Sub ReviseMargin(ByVal sender As Object, ByVal e As EventArgs)
        'Me.DefaultPageSettings.Margins = MarginForm.GetRevisedMargin(Me.DefaultPageSettings.Margins)
        Me.PageSetupDialog.ShowDialog()
    End Sub

    Private Sub PrintInPreviewWindow(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        If (Me.PrintDialog.ShowDialog = DialogResult.OK) Then

        End If
    End Sub




   
    
End Class
