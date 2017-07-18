Public Class fmBase1
    Public DNS_SYS As String = INI_Read("CONFIG", "SET", "DNS_SYS")
    Public DNS_ACC As String = INI_Read("CONFIG", "SET", "DNS_ACC")
    Public DNS_AUTH As String = INI_Read("CONFIG", "SET", "DNS_AUTH")
    Public DNS_FUND As String = INI_Read("CONFIG", "SET", "DNS_FUND")
    Public mastconn As String = INI_Read("CONFIG", "SET", "DNS_ACC")
End Class