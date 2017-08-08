Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Public Class PGMGlobal

    Public Shared Function GetConfigColor(ByVal key As String, ByVal defaultColor As Color) As Color
        Dim sLeft As String = Strings.Trim(ConfigurationManager.AppSettings(key))
        If (StringType.StrCmp(sLeft, String.Empty, False) = 0) Then
            Return defaultColor
        End If
        If Information.IsNumeric(sLeft) Then
            Return Color.FromArgb(IntegerType.FromString(sLeft))
        End If
        Return Color.FromName(sLeft)
    End Function

    Public Shared Function GetConfigString(ByVal key As String, ByVal defaultValue As String) As String
        'Dim sLeft As String = Strings.Trim(ConfigurationSettings.AppSettings.Item(key))
        Dim sLeft As String = Strings.Trim(ConfigurationManager.AppSettings(key))
        If (StringType.StrCmp(sLeft, String.Empty, False) = 0) Then
            Return defaultValue
        End If
        Return sLeft
    End Function

    Public Shared gCopyDataRowSeparator As String = PGMGlobal.GetConfigString("複製整列資料或是整個Grid資料所使用的欄分隔符號", "  ")
    Public Shared gDataGridAlternativeBackColor As Color = PGMGlobal.GetConfigColor("DataGrid資料列的交替背景顏色", Color.LightCyan)
    Public Shared gDataGridCaptionForeColor As Color = PGMGlobal.GetConfigColor("DataGrid資料表標頭的前景顏色", Color.Blue)
    Public Shared gDataGridHeaderForeColor As Color = PGMGlobal.GetConfigColor("DataGrid欄標頭的前景顏色", Color.DarkGreen)
    Public Shared gDataGridSelectionBackColor As Color = PGMGlobal.GetConfigColor("DataGrid被選取資料列的背景顏色", Color.LightGreen)
    Public Shared gPressEnterToAddOrRevise As Boolean = False
    Public Shared gPressEnterToOtherAction As Boolean = False
    Public Shared gPressEnterToQuery As Boolean = False
    Public Shared gQueryPanelBackColor As Color = PGMGlobal.GetConfigColor("查詢面板的背景顏色", Color.Bisque)
End Class
