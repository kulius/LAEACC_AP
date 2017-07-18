Imports System.Globalization
Public Class BAIL120
    Dim LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim mydataset As DataSet

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim response As MsgBoxStyle

        response = (MsgBox("是否刪除!", MsgBoxStyle.YesNoCancel, "刪除保證金保證品"))
        If response = MsgBoxResult.Cancel Then
            Exit Sub
        Else
            Call delBail(response)
        End If
    End Sub

    Private Sub delBail(ByVal nowDeleting As MsgBoxStyle)
        If nowDeleting = MsgBoxResult.Yes Then
            Dim retstr As String
            Dim intI As Integer
            Dim mydatasetp As DataSet
            Dim mydatasetInq As DataSet
            Dim balancedate As String
            Dim datarowp() As DataRow
            balancedate = dtpRefund.Value.ToString("yyyy/M/d", CultureInfo.InvariantCulture)
            '刪除保證金資料(標示BALANCE欄位為Y,表示已結算)
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='1' and rpdate<'" & balancedate & "' and (balance is null or balance='')  group by engno order by engno" '依工程編號找出保證金收的記錄
            mydataset = openmember("", "bailf010", sqlstr)
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='2' and rpdate<'" & balancedate & "' and (balance is null or balance='') group by engno order by engno" '依工程編號找出保證金支的記錄

            mydatasetp = openmember("", "bailf010", sqlstr)


            For intI = 0 To (mydataset.Tables("bailf010").Rows.Count - 1)
                datarowp = mydatasetp.Tables("bailf010").Select("engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "'")     '依工程代號找出已付的金額
                If datarowp.Length > 0 Then
                    mydataset.Tables("bailf010").Rows(intI).Item("amt") = mydataset.Tables("bailf010").Rows(intI).Item("amt") - datarowp(0).Item("amt")  '已收金額減去已付金額
                End If

                If mydataset.Tables("bailf010").Rows(intI).Item("amt") = 0 Then
                    GenUpdsql("balance", "Y", "T")
                    sqlstr = "update bailf010 set " & genupdfunc & " where engno='" & mydataset.Tables("bailf010").Rows(intI).Item("engno") & "' and rpdate < '" & balancedate & "'"
                    retstr = runsql(mastconn, sqlstr)
                    If retstr <> "sqlok" Then
                        MsgBox("資料新增失敗，請洽詢程式設計人員!")
                        Exit Sub
                    End If
                End If
            Next
            mydataset.Clear()
            mydatasetp.Clear()
            '刪除保證品資料(標示BALANCE欄位為Y,表示已結算)
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='1' and rpdate<'" & balancedate & "' and  (balance is null or balance='') group by engno order by engno" '依工程編號找出保證品收的記錄

            mydataset = openmember("", "bailf020", sqlstr)
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='2' and rpdate<'" & balancedate & "' and (balance is null or balance='') group by engno order by engno" '依工程編號找出保證品支的記錄

            mydatasetp = openmember("", "bailf020", sqlstr)


            For intI = 0 To (mydataset.Tables("bailf020").Rows.Count - 1)
                datarowp = mydatasetp.Tables("bailf020").Select("engno='" & mydataset.Tables("BAILF020").Rows(intI).Item("engno") & "'")     '依工程代號找出已付的金額
                If datarowp.Length > 0 Then
                    mydataset.Tables("bailf020").Rows(intI).Item("amt") = mydataset.Tables("bailf020").Rows(intI).Item("amt") - datarowp(0).Item("amt")  '已收金額減去已付金額
                End If

                If mydataset.Tables("bailf020").Rows(intI).Item("amt") = 0 Then
                    GenUpdsql("balance", "Y", "T")
                    sqlstr = "update bailf020 set " & genupdfunc & " where engno='" & mydataset.Tables("bailf020").Rows(intI).Item("engno") & "' and rpdate<'" & balancedate & "'"
                    retstr = runsql(mastconn, sqlstr)
                    If retstr <> "sqlok" Then
                        MsgBox("資料新增失敗，請洽詢程式設計人員!")
                        Exit Sub
                    End If
                End If
            Next
        End If
        MsgBox("資料刪除完成!")

    End Sub

    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub

    Private Sub frmBail120_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpRefund.Value = TransPara.TransP("UserDATE")
    End Sub
End Class
