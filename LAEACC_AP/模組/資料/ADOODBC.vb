'::::::::::::::::::
'::: 資料庫處理 :::
'::::::::::::::::::
Imports System.Data
Imports System.Data.SqlClient

Module ADOODBC
    '資料庫公用變數
    Dim objConDB As SqlConnection
    Dim objCmdDB As SqlCommand
    Dim objDRDB As SqlDataReader
    Dim objDADB As SqlDataAdapter
    Dim objDSDB As DataSet
    Dim strSQLDB As String

    Sub MyADOODBC()

    End Sub

#Region "資料測試"
    ''' <summary>
    ''' 資料庫連結字串測試
    ''' </summary>
    ''' <param name="strDNS">連結字串</param>
    ''' <returns>是否成功連結</returns>
    Function dbConnectionTest(ByVal strDNS As String) As Boolean
        Dim blnCheck As Boolean = False

        Try
            objConDB = New SqlConnection(strDNS)
            objConDB.Open()
            objConDB.Close()   '關閉連結
            objConDB.Dispose()
            objConDB = Nothing '移除指標

            blnCheck = True

        Catch ex As Exception
            blnCheck = False
        End Try

        Return blnCheck
    End Function
#End Region

#Region "資料處理"
    ''' <summary>
    ''' 執行SQL語法
    ''' </summary>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strSQL">SQL語法</param>
    ''' <returns>True:執行成功 False:執行失敗</returns>    
    Function dbExecute(ByVal strDNS As String, ByVal strSQL As String) As Boolean
        Dim blnCheck As Boolean = False

        Try
            objConDB = New SqlConnection(strDNS)
            objConDB.Open()

            strSQLDB = strSQL

            objCmdDB = New SqlCommand(strSQLDB, objConDB)
            objCmdDB.ExecuteNonQuery()

            objConDB.Close()   '關閉連結
            objCmdDB.Dispose() '手動釋放資源
            objConDB.Dispose()
            objConDB = Nothing '移除指標

            blnCheck = True
        Catch ex As Exception
            blnCheck = False
        End Try

        Return blnCheck
    End Function

    ''' <summary>
    ''' 新增資料
    ''' </summary>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strTable">寫入資料表</param>
    ''' <param name="strRowName">寫入欄位</param>
    ''' <param name="strRowValue">寫入資料</param>
    ''' <returns>True:寫入成功 False:寫入失敗</returns>    
    Function dbInsert(ByVal strDNS As String, ByVal strTable As String, ByVal strRowName As String, ByVal strRowValue As String) As Boolean
        Dim blnCheck As Boolean = False

        Try            
            objConDB = New SqlConnection(strDNS)
            objConDB.Open()

            strSQLDB = "INSERT INTO " & strTable & " WITH (HOLDLOCK) (" & strRowName & ")"
            strSQLDB &= " VALUES (" & strRowValue & ")"

            objCmdDB = New SqlCommand(strSQLDB, objConDB)
            objCmdDB.ExecuteNonQuery()

            objConDB.Close()   '關閉連結
            objCmdDB.Dispose() '手動釋放資源
            objConDB.Dispose()
            objConDB = Nothing '移除指標

            blnCheck = True
        Catch ex As Exception
            blnCheck = False
            MsgBox(strSQLDB)
        End Try

        Return blnCheck
    End Function

    ''' <summary>
    ''' 修改資料
    ''' </summary>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strTable">修改資料表</param>
    ''' <param name="strEditRow">修改欄位</param>
    ''' <param name="strWhere">修改條件值</param>
    ''' <returns>True:修改成功 False:修改失敗</returns>    
    Function dbEdit(ByVal strDNS As String, ByVal strTable As String, ByVal strEditRow As String, ByVal strWhere As String) As Boolean
        Dim blnCheck As Boolean = False

        Try
            objConDB = New SqlConnection(strDNS)
            objConDB.Open()

            strSQLDB = "UPDATE " & strTable & " SET"
            strSQLDB &= " " & strEditRow
            strSQLDB &= " WHERE " & strWhere

            objCmdDB = New SqlCommand(strSQLDB, objConDB)
            objCmdDB.ExecuteNonQuery()

            objConDB.Close()   '關閉連結
            objCmdDB.Dispose() '手動釋放資源
            objConDB.Dispose()
            objConDB = Nothing '移除指標

            blnCheck = True
        Catch ex As Exception
            blnCheck = False
        End Try

        Return blnCheck
    End Function

    ''' <summary>
    ''' 刪除資料
    ''' </summary>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strTable">刪除資料表</param>
    ''' <param name="strWhere">刪除條件值</param>
    ''' <returns>True:刪除成功 False:刪除失敗</returns>    
    Function dbDelete(ByVal strDNS As String, ByVal strTable As String, ByVal strWhere As String) As Boolean
        Dim blnCheck As Boolean = False

        Try
            objConDB = New SqlConnection(strDNS)
            objConDB.Open()

            strSQLDB = "DELETE FROM " & strTable
            strSQLDB &= " WHERE " & strWhere

            objCmdDB = New SqlCommand(strSQLDB, objConDB)
            objCmdDB.ExecuteNonQuery()

            objConDB.Close()   '關閉連結
            objCmdDB.Dispose() '手動釋放資源
            objConDB.Dispose()
            objConDB = Nothing '移除指標

            blnCheck = True
        Catch ex As Exception
            blnCheck = False
        End Try

        Return blnCheck
    End Function
#End Region

#Region "資料查詢"
    ''' <summary>
    ''' 是否有相同關鍵值資料
    ''' </summary>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strTable">查詢資料表</param>
    ''' <param name="strWhere">查詢條件值</param>
    ''' <returns>True:有相同資料 False:無相同資料</returns>
    Function dbDataCheck(ByVal strDNS As String, ByVal strTable As String, ByVal strWhere As String) As Boolean
        Dim blnCheck As Boolean = False

        '開啟查詢
        objConDB = New SqlConnection(strDNS)
        objConDB.Open()

        strSQLDB = "SELECT * FROM " & strTable
        strSQLDB &= " WHERE " & strWhere

        objCmdDB = New SqlCommand(strSQLDB, objConDB)
        objDRDB = objCmdDB.ExecuteReader()

        If objDRDB.Read Then blnCheck = True

        objConDB.Close()   '關閉連結
        objCmdDB.Dispose() '手動釋放資源
        objConDB.Dispose()
        objConDB = Nothing '移除指標

        Return blnCheck
    End Function

    ''' <summary>
    ''' 取得單一欄位
    ''' </summary>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strTable">查詢資料表</param>
    ''' <param name="strRow">欄位值名稱</param>
    ''' <param name="strWhere">查詢條件值(可省略)</param>
    ''' <param name="strOrderBy">自訂排序(可省略)</param>
    ''' <returns>傳回欄位值(String)</returns>
    Function dbGetSingleRow(ByVal strDNS As String, ByVal strTable As String, ByVal strRow As String, Optional strWhere As String = "1=1", Optional ByVal strOrderBy As String = "") As String
        Dim strString As String = ""

        '開啟查詢
        objConDB = New SqlConnection(strDNS)
        objConDB.Open()

        strSQLDB = "SELECT " & strRow & " FROM " & strTable
        strSQLDB &= " WHERE " & strWhere
        If strOrderBy <> "" Then strSQLDB &= " ORDER BY " & strOrderBy

        objCmdDB = New SqlCommand(strSQLDB, objConDB)
        objDRDB = objCmdDB.ExecuteReader()

        If objDRDB.Read Then strString = objDRDB(strRow).ToString

        objDRDB.Close()    '關閉連結
        objConDB.Close()
        objCmdDB.Dispose() '手動釋放資源
        objConDB.Dispose()
        objConDB = Nothing '移除指標

        Return strString
    End Function
#End Region

#Region "資料計算"
    ''' <summary>
    ''' 數量計算(條件式)
    ''' </summary>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strTable">查詢資料表</param>    
    ''' <param name="strAsRow">計算後數值欄位名(預設：intCount)(可省略)</param>
    ''' <param name="strWhere">查詢條件值(可省略)</param>
    ''' <returns>傳回數量(Integer)</returns>
    Function dbCount(ByVal strDNS As String, ByVal strTable As String, Optional strAsRow As String = "intCount", Optional strWhere As String = "1=1") As Integer
        Dim intValue As Integer = 0

        '開啟查詢
        objConDB = New SqlConnection(strDNS)
        objConDB.Open()

        strSQLDB = "SELECT COUNT(*) AS " & strAsRow & " FROM " & strTable
        strSQLDB &= " WHERE " & strWhere

        objCmdDB = New SqlCommand(strSQLDB, objConDB)
        objDRDB = objCmdDB.ExecuteReader()

        If objDRDB.Read Then intValue = IIf(IsNumeric(objDRDB(strAsRow).ToString) = False, 0, objDRDB(strAsRow).ToString)

        objConDB.Close()   '關閉連結
        objCmdDB.Dispose() '手動釋放資源
        objConDB.Dispose()
        objConDB = Nothing '移除指標

        Return intValue
    End Function

    ''' <summary>
    ''' 加總計算(條件式)
    ''' </summary>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strTable">查詢資料表</param>
    ''' <param name="strSumRow">欲加總欄位</param>
    ''' <param name="strAsRow">加總後數值欄位名(預設：intSum)(可省略)</param>
    ''' <param name="strWhere">查詢條件值(可省略)</param>
    ''' <returns>傳回加總值(Double)</returns>
    Function dbSum(ByVal strDNS As String, ByVal strTable As String, ByVal strSumRow As String, Optional strAsRow As String = "intSum", Optional strWhere As String = "1=1") As Double
        Dim intValue As Double = 0

        '開啟查詢
        objConDB = New SqlConnection(strDNS)
        objConDB.Open()

        strSQLDB = "SELECT SUM(" & strSumRow & ") AS " & strAsRow & " FROM " & strTable
        strSQLDB &= " WHERE " & strWhere

        objCmdDB = New SqlCommand(strSQLDB, objConDB)
        objDRDB = objCmdDB.ExecuteReader()

        If objDRDB.Read Then intValue = IIf(IsNumeric(objDRDB(strAsRow).ToString) = False, 0, objDRDB(strAsRow).ToString)

        objConDB.Close()   '關閉連結
        objCmdDB.Dispose() '手動釋放資源
        objConDB.Dispose()
        objConDB = Nothing '移除指標

        Return intValue
    End Function

    ''' <summary>
    ''' 最大值、最小值計算(條件式)
    ''' </summary>
    ''' <param name="strDNS">資料庫連結字串</param>
    ''' <param name="strTable">查詢資料表</param>
    ''' <param name="strType">判斷種類(Max：最大值 Min：最小值)</param>
    ''' <param name="strMaxRow">欲判斷欄位</param>
    ''' <param name="strAsRow">判斷後欄位名(預設：strData)(可省略)</param>
    ''' <param name="strWhere">查詢條件值(可省略)</param>
    ''' <returns>傳回判斷值(String)</returns>
    Function dbMaxMin(ByVal strDNS As String, ByVal strTable As String, ByVal strType As String, ByVal strMaxRow As String, Optional strAsRow As String = "strData", Optional ByVal strWhere As String = "1=1") As String
        Dim strString As String = ""

        '開啟查詢
        objConDB = New SqlConnection(strDNS)
        objConDB.Open()

        strSQLDB = "SELECT " & strType & "(" & strMaxRow & ") AS " & strAsRow & " FROM " & strTable
        strSQLDB &= " WHERE " & strWhere

        objCmdDB = New SqlCommand(strSQLDB, objConDB)
        objDRDB = objCmdDB.ExecuteReader()

        If objDRDB.Read Then strString = objDRDB(strAsRow).ToString

        objDRDB.Close()    '關閉連結
        objConDB.Close()
        objCmdDB.Dispose() '手動釋放資源
        objConDB.Dispose()
        objConDB = Nothing '移除指標

        Return strString
    End Function
#End Region


End Module