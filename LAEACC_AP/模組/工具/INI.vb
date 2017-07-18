':::::::::::::::::
'::: INI檔處理 :::
':::::::::::::::::
Imports System.Text
Imports System.Runtime.InteropServices

Module INI
    '-- 讀*.ini檔 --
    Public Declare Function GetPrivateProfileString Lib "kernel32" _
     Alias "GetPrivateProfileStringA" ( _
     <MarshalAs(UnmanagedType.LPStr)> ByVal lpApplicationName As String, _
     <MarshalAs(UnmanagedType.LPStr)> ByVal lpKeyName As String, _
     <MarshalAs(UnmanagedType.LPStr)> ByVal lpDefault As String, _
     <MarshalAs(UnmanagedType.LPStr)> ByVal lpReturnedString As StringBuilder, _
     ByVal nSize As UInt32, _
     <MarshalAs(UnmanagedType.LPStr)> ByVal lpFileName As String) As UInt32

    '-- 寫*.ini檔 --
    Public Declare Function WritePrivateProfileString Lib "kernel32" _
     Alias "WritePrivateProfileStringA" ( _
     <MarshalAs(UnmanagedType.LPStr)> ByVal lpApplicationName As String, _
     <MarshalAs(UnmanagedType.LPStr)> ByVal lpKeyName As String, _
     <MarshalAs(UnmanagedType.LPStr)> ByVal lpReturnedString As StringBuilder, _
     <MarshalAs(UnmanagedType.LPStr)> ByVal lpFileName As String) As UInt32

    Public Structure SYSTEM_INFO
        Dim dwOemID As Integer
        Dim wProcessorArchitecture As Integer
        Dim wReserved As Integer
        Dim dwPageSize As Integer
        Dim lpMinimumApplicationAddress As Integer
        Dim lpMaximumApplicationAddress As Integer
        Dim dwActiveProcessorMask As Integer
        Dim dwNumberOrfProcessors As Integer
        Dim dwProcessorType As Integer '序號
        Dim dwAllocationGranularity As Short
        Dim wProcessLevel As Short
        Dim wProcessorRevision As Short
    End Structure

    '-- 公用變數 --
    Dim lReturnLen As UInt32

    ''' <summary>
    ''' 讀取
    ''' </summary>
    ''' <param name="strFile">INI檔案名稱</param>
    ''' <param name="strBCol">項目</param>
    ''' <param name="strCol">欄位</param>
    ''' <returns>回傳值(String)</returns>    
    Function INI_Read(ByVal strFile As String, ByVal strBCol As String, ByVal strCol As String) As String
        Dim result As String '回傳變數值

        Dim sinifilename As String = Application.StartupPath & "\INI\" & strFile & ".ini" '*.ini檔案路徑

        Dim sSection As String = strBCol '段名
        Dim sKey As String = strCol '值名

        Dim sKeyValue As New StringBuilder(512)
        Dim nSize As UInt32 = Convert.ToUInt32(512)


        '-- 讀取值 --
        lReturnLen = GetPrivateProfileString(sSection, sKey, "", sKeyValue, nSize, sinifilename)
        result = sKeyValue.ToString

        Return result
    End Function
    ''' <summary>
    ''' 修改
    ''' </summary>
    ''' <param name="strFile">INI檔案名稱</param>
    ''' <param name="strBCol">項目</param>
    ''' <param name="strCol">欄位</param>
    ''' <param name="strChangeValue">異動值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function INI_Write(ByVal strFile As String, ByVal strBCol As String, ByVal strCol As String, ByVal strChangeValue As String) As Boolean
        Dim result As Boolean = False

        Dim sinifilename As String = Application.StartupPath & "\INI\" & strFile & ".ini" '*.ini檔案路徑

        Dim sSection As String = strBCol '段名
        Dim sKey As String = strCol '值名

        Dim sKeyValue As New StringBuilder(512)
        Dim nSize As UInt32 = Convert.ToUInt32(512)


        '-- 修改值 --
        Try
            sKeyValue = sKeyValue.Append(strChangeValue)
            lReturnLen = WritePrivateProfileString(sSection, sKey, sKeyValue, sinifilename)
            result = True
        Catch ex As Exception
            result = False
        End Try

        Return result
    End Function
End Module
