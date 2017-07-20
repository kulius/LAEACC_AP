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

        Me.PrintPreviewDialog.Text = ("預覽列印-新版")

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
        'Catch exception1 As InvalidPrinterException
        '    ProjectData.SetProjectError(exception1)
        '    Dim exception As InvalidPrinterException = exception1
        '    If Not Environment.UserInteractive Then
        '        Throw exception
        '    End If
        '    Interaction.MsgBox(((("印表機名稱：" & Me.PrinterSettings.PrinterName.ToString & ChrW(13) & ChrW(10)) & "列印發生問題，可能原因如下：" & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10) & "1.沒有安裝任何印表機驅動程式" & ChrW(13) & ChrW(10)) & "2.指定的印表機名稱不存在" & ChrW(13) & ChrW(10) & "3.指定的印表機名稱其驅動程式損毀，請重新安裝" & ChrW(13) & ChrW(10)), 0, Nothing)
        '    'Dim info As New LogInfo("", 1, "FPPrinter.PrintInPreviewWindow")
        '    'ExceptionManager.Report(info, exception)
        '    ProjectData.ClearProjectError()
        'Catch exception3 As Win32Exception
        '    ProjectData.SetProjectError(exception3)
        '    Dim exception2 As Win32Exception = exception3
        '    If Not Environment.UserInteractive Then
        '        Throw exception2
        '    End If
        '    Interaction.MsgBox((("印表機名稱：" & Me.PrinterSettings.PrinterName.ToString & ChrW(13) & ChrW(10)) & "列印發生問題，可能是指定的印表機名稱其驅動程式損毀，請重新安裝！" & ChrW(13) & ChrW(10)), 0, Nothing)
        '    'Dim info2 As New LogInfo("", 1, "FPPrinter.PrintInPreviewWindow")
        '    'ExceptionManager.Report(info2, exception2)
        '    ProjectData.ClearProjectError()
        'End Try
    End Sub

    Private Sub SetPrintPage()
        Select Case Me.mPD.PrinterSettings.PrintRange
            Case PrintRange.AllPages
                Me.IsPrintAllPages = True
                Exit Select
            Case PrintRange.SomePages
                Me.AddPageNo(Me.mPD.PrinterSettings.FromPage, Me.mPD.PrinterSettings.ToPage, True)
                Exit Select
        End Select
    End Sub




    Public mPD As PrintDocument
    Private Shared DefaultPageSettings As PageSettings
    Public Shared PrinterSettings As PrinterSettings
    
End Class
