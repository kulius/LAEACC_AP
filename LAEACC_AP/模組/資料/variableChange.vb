'::::::::::::::::::
'::: 變數值轉換 :::
'::::::::::::::::::
Imports System.Data
Imports System.Data.SqlClient

Module variableChange
    '--資料庫公用變數--
    Dim objConVC As SqlConnection
    Dim objCmdVC As SqlCommand
    Dim objDRVC As SqlDataReader
    Dim strSQLVC As String

    Sub MyVariableChange()

    End Sub

#Region "字串補數"
    ''' <summary>
    ''' 左補(0)
    ''' </summary>
    ''' <param name="intValue">需補位數</param>
    ''' <param name="strValue">字串</param>
    ''' <returns>補碼字串(String)</returns>
    Function strZero(ByVal intValue As Integer, ByVal strValue As String) As String
        Dim strString As String = strValue

        '開啟補數
        For I As Integer = Len(strString) To intValue - 1 Step 1
            strString = "0" & strString
        Next

        Return strString
    End Function
    ''' <summary>
    ''' 右補(空白)
    ''' </summary>
    ''' <param name="intValue">需補位數</param>
    ''' <param name="strValue">字串</param>
    ''' <returns>補碼字串(String)</returns>
    Function strEmpty(ByVal intValue As Integer, ByVal strValue As String) As String
        Dim strString As String = strValue

        '開啟補數
        For I As Integer = Len(strString) To intValue - 1 Step 1
            strString = strString & " "
        Next

        Return strString
    End Function
#End Region

#Region "系統編號"
    ''' <summary>
    ''' 系統自動編號
    ''' </summary>
    ''' <param name="Type">種類前置代碼</param>
    ''' <param name="Length">數字位數長度</param>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strTable">資料表</param>
    ''' <param name="strRow">編號欄位名</param>
    ''' <param name="strWhere">條件值(可省略)</param>
    ''' <returns>系統編號(String)</returns>
    Function AutoNumber(ByVal Type As String, ByVal Length As String, ByVal strDNS As String, ByVal strTable As String, ByVal strRow As String, Optional strWhere As String = "1=1") As String
        Dim strString As String = ""

        '開啟查詢
        objConVC = New SqlConnection(strDNS)
        objConVC.Open()

        strSQLVC = "SELECT MAX(" & strRow & ") As strMax FROM " & strTable
        strSQLVC &= " WHERE " & strWhere

        objCmdVC = New SqlCommand(strSQLVC, objConVC)
        objDRVC = objCmdVC.ExecuteReader()

        If objDRVC.Read Then strString = IIf(objDRVC("strMax").ToString = "", "1", CInt(objDRVC("strMax").ToString) + 1)

        strString = Type & strZero(Length, strString)

        objConVC.Close()   '關閉連結
        objCmdVC.Dispose() '手動釋放資源
        objConVC.Dispose()
        objConVC = Nothing '移除指標

        Return strString
    End Function
#End Region
End Module
