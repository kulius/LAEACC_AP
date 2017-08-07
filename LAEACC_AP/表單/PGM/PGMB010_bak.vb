Public Class PGMB010_bak
    Dim gPF As ProgressForm = New ProgressForm
    'Private Sub PGMB010_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    txtQueryKindNo.Focus()
    'End Sub

    'Private Sub btnQuery_Click(sender As Object, e As EventArgs) Handles btnQuery.Click
    '    Query()
    'End Sub

    'Private Sub Query()
    '    '先取消選取,以免等下查詢出來造成重複的選取項目
    '    If dgData.CurrentRowIndex <> -1 Then
    '        dgData.UnSelect(dgData.CurrentRowIndex)
    '    End If
    '    Dim kindno, kindno2, name, unit, material, useyear, rowFilter As String
    '    kindno = Trim(txtQueryKindNo.Text)
    '    kindno2 = Trim(txtQueryKindNo2.Text)
    '    name = Trim(txtQueryName.Text)
    '    unit = Trim(cboQueryUnit.Text)
    '    material = Trim(cboQueryMaterial.Text)
    '    useyear = Trim(cboQueryUseyear.Text)
    '    If Not IsNumeric(kindno) And kindno <> "" Then
    '        MsgBox("起始編號只能輸入數字")
    '        txtQueryKindNo.SelectAll()
    '        txtQueryKindNo.Focus()
    '        Exit Sub
    '    End If
    '    If Not IsNumeric(kindno2) And kindno2 <> "" Then
    '        MsgBox("終止編號只能輸入數字")
    '        txtQueryKindNo2.SelectAll()
    '        txtQueryKindNo2.Focus()
    '        Exit Sub
    '    End If
    '    If kindno > kindno2 And (kindno <> "" And kindno2 <> "") Then
    '        MsgBox("起始編號必須小於終止編號")
    '        txtQueryKindNo.SelectAll()
    '        txtQueryKindNo.Focus()
    '        Exit Sub
    '    End If
    '    If (Not IsNumeric(useyear) And useyear <> "") Or useyear.IndexOf("-") >= 0 Or useyear.IndexOf(".") >= 0 Then
    '        MsgBox("使用年限必須輸入正整數")
    '        cboQueryUseyear.Focus()
    '        Exit Sub
    '    End If
    '    btnQuery.Enabled = False
    '    gPF.ShowQuery()
    '    mDT = gPGMLib.GetAllPGKind(True)
    '    '資料有改變就要去資料庫取得最新資料
    '    'If mIsDataChanged Then
    '    '    mDT = gPGMLib.GetAllPGKind(True)
    '    '    mIsDataChanged = False
    '    'Else
    '    '    If mDT Is Nothing Then
    '    '        mDT = gPGMLib.GetAllPGKind
    '    '    End If
    '    'End If

    '    mDV.Table = mDT
    '    rowFilter = "(kindno >= '" & kindno & "' or '" & kindno & "'='')"
    '    rowFilter += " and (kindno <= '" & kindno2 & "' or '" & kindno2 & "'='')"
    '    rowFilter += " and (name like '%" & name & "%'  or '" & name & "'='')"
    '    rowFilter += " and (unit like '%" & unit & "%' or '" & unit & "'='')"
    '    rowFilter += " and (material like '%" & material & "%' or '" & material & "'='')"
    '    rowFilter += " and (useyear=" & IIf(useyear = "", 0, useyear) & " or '" & useyear & "'='')"
    '    mDV.RowFilter = rowFilter
    '    dgData.DataSource = mDV
    '    ClearData()
    '    lblTotal.Text = mDV.Count
    '    If mDV.Count = 0 Then
    '        MsgBox("找不到資料，請重新設定查詢條件", , mMsgTitle)
    '        '使大部分控制項失效
    '        DisableControl()
    '    Else
    '        '使大部分控制項生效,並選擇第一筆資料
    '        EnableControl()
    '    End If
    '    tcTab.SelectedIndex = 0
    '    btnQuery.Enabled = True
    '    gPF.HideMe()
    'End Sub
End Class
