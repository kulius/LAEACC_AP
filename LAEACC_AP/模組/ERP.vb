Imports System.Data.SqlClient

Module ERP
#Region "@資料庫共用變數@"
    Dim strCSQL As String '查詢數量
    Dim strSSQL As String '查詢資料

    '程序專用*****
    Dim objCon99 As SqlConnection
    Dim objCmd99 As SqlCommand
    Dim objDR99 As SqlDataReader
    Dim strSQL99 As String
#End Region
#Region "@固定公用變數@"
    Dim I As Integer '累進變數
    Dim strMessage As String = "" '訊息字串
    Dim strIRow, strIValue, strUValue, strWValue As String '資料處理參數(新增欄位；新增資料；異動資料；條件)      
#End Region

#Region "系統編號"
    '系統單號
    Function ERP_AutoNo(ByVal DNS As String, ByVal strPage As String, ByVal strTable As String, ByVal strRow As String) As String
        Dim strString As String = ""
        Dim strMaxNo As String = dbMaxMin(DNS, strTable, "Max", strRow, "strMax", strRow & " LIKE '" & strPage & "%'")
        Dim intMaxNo As Integer = 1
        If strMaxNo <> "" Then intMaxNo = Mid(strMaxNo, 5, Len(strMaxNo) - 4) + 1

        strString = strPage & strZero(6, intMaxNo)

        Return strString
    End Function

    '貨單序號
    Function ERP_AutoNo_貨單序號(ByVal DNS As String, ByVal strPage As String, ByVal strTable As String, ByVal strRow As String) As String
        Dim strString As String = ""
        Dim strMaxNo As String = dbMaxMin(DNS, strTable, "Max", strRow, "strMax", strRow & " LIKE '" & strPage & "%'")
        Dim intMaxNo As Integer = 1
        If strMaxNo <> "" Then intMaxNo = Mid(strMaxNo, Len(strPage) + 1, 4) + 1

        strString = strPage & strZero(4, intMaxNo)

        Return strString
    End Function

    '貨單序號
    Function ERP_AutoNo_憑單號碼(ByVal DNS As String, ByVal strPage As String, ByVal strTable As String, ByVal strRow As String) As String
        Dim strString As String = ""
        Dim strMaxNo As String = dbMaxMin(DNS, strTable, "Max", strRow, "strMax", strRow & " LIKE '" & strPage & "%'")
        Dim intMaxNo As Integer = 1
        If strMaxNo <> "" Then intMaxNo = Mid(strMaxNo, 8, 4) + 1

        strString = strPage & strZero(4, intMaxNo)

        Return strString
    End Function

    '長度編號
    Function ERP_AutoNo_產品編號(ByVal DNS As String) As String
        Dim strString As String = ""
        Dim strMaxNo As String = dbMaxMin(DNS, "s_一層代碼檔", "Max", "一層代碼", "strMax", "類別='S03N0001' and 一層代碼 <'800'")
        Dim intMaxNo As Integer = 1
        If strMaxNo <> "" Then intMaxNo = strMaxNo + 1

        strString = strZero(3, intMaxNo)

        Return strString
    End Function
#End Region
End Module
