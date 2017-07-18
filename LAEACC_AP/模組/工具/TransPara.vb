
Module TransPara
    '-- 讀*.ini檔 --
   Dim para(20, 2)

    Public Property TransP(ByVal paraname)
        Get
            Return Getpara(paraname)
        End Get
        Set(ByVal Value)
            Call SavePara(paraname, Value)
        End Set
    End Property
    Sub SavePara(ByVal paraname As String, ByVal paravalue As String)
        Dim ii As Integer
        For ii = 0 To 19
            If para(ii, 0) = paraname Then
                para(ii, 1) = paravalue
                Exit Sub
            End If
            If para(ii, 0) = Nothing Then
                para(ii, 0) = paraname
                para(ii, 1) = paravalue
                Exit Sub
            End If
        Next
    End Sub
    Function Getpara(ByVal paraname)

        Dim ii As Integer, uname As String
        uname = UCase(paraname)
        For ii = 0 To 19
            If UCase(para(ii, 0)) = Uname Then
                Return para(ii, 1)
                Exit Function
            End If
        Next
    End Function
End Module
