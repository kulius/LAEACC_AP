Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class fmBase2
    Public DNS As String = INI_Read("CONFIG", "SET", "DNS") 'DNS設定值
    Public FIRM As String = fmMain.FIRM 'DNS設定值
    Public FIRMCODE As String = fmMain.FIRMCODE 'DNS設定值

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
#Region "@底層變數@"
    Public FStatus As Byte '資料狀態
    Public ProgramID As String '取得目前作業的程式名稱

    Public FAllowAddNew As Boolean = True '允許更新
    Public FAllowEdit As Boolean = True '允許修改
    Public FAllowDelete As Boolean = True '允許刪除
    Public FAllowPrint As Boolean = True '允許列印表單

    Public intCol, intRow As Integer '目前於DataGridView點選的位置
#End Region

#Region "@Form及功能操作@"
    Private Sub fmBase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyTableStatus = 0 '預設狀態為瀏覽模式
        SetButtons() '所有按鈕的狀態
        SetControls(0) '設定所有輸入控制項的唯讀狀態
        ProgramID = Me.Name '取得程式名稱

        '視窗位置
        Me.Left = 0
        Me.Top = 0
        'Me.Width = Bounds_Width()
    End Sub
    Private Sub fmBase_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MyTableStatus <> 0 Then
            If MessageBox.Show("資料尚未儲存，仍要離開本作業嗎？",
                "資料未儲存", MessageBoxButtons.YesNo, _
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) _
                = Windows.Forms.DialogResult.No Then

                e.Cancel = True
            Else
                e.Cancel = False
            End If
        End If
    End Sub

    '離開
    Private Sub butExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butExit.Click
        If MyTableStatus <> 0 Then
            If MessageBox.Show("資料尚未儲存，仍要離開本作業嗎？", "資料未儲存",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) _
               = Windows.Forms.DialogResult.No Then

                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    '第一筆
    Private Sub butFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butFirst.Click
        SetButtons()

        '按下butFirst按鈕
        If BeforeScroll() Then
            '找出指定資料表的第一筆記錄
            BeforeLoad()
            FlagMoveSeat(1)
            AfterLoad()

            MyTableStatus = 0
            AfterScroll()
        End If
    End Sub
    '上一筆
    Private Sub butPrior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butPrior.Click
        SetButtons()

        '按下butPrior按鈕
        If BeforeScroll() Then
            '找出指定資料表的上一筆記錄
            BeforeLoad()
            FlagMoveSeat(2)
            AfterLoad()

            MyTableStatus = 0
            AfterScroll()
        End If
    End Sub
    '下一筆
    Private Sub butNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butNext.Click
        SetButtons()

        '按下butNext按鈕
        If BeforeScroll() Then
            '找出指定資料表的第一筆記錄
            BeforeLoad()
            FlagMoveSeat(3)
            AfterLoad()

            MyTableStatus = 0
            AfterScroll()
        End If
    End Sub
    '最未筆
    Private Sub butLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butLast.Click
        SetButtons()

        '按下butLast按鈕
        If BeforeScroll() Then
            '找出指定資料表的第一筆記錄
            BeforeLoad()
            FlagMoveSeat(4)
            AfterLoad()

            MyTableStatus = 0
            AfterScroll()
        End If
    End Sub

    '儲存
    Private Sub butSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSave.Click
        '按下butSave按鈕
        If BeforeEndEdit() Then
            Dim PrevTableStatus As String = MyTableStatus
            Dim SaveStatus As Boolean = True

            Try
                '判斷程序為新增或修改
                If PrevTableStatus = "1" Then SaveStatus = InsertData() '新增
                If PrevTableStatus = "2" Then SaveStatus = UpdateData() '修改

                If SaveStatus Then
                    MyTableStatus = 0
                    SetControls(0)
                    FillData("")
                    AfterEndEdit()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "操作錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MyTableStatus = PrevTableStatus
            End Try
        End If

        SetButtons()
    End Sub
    '還原
    Private Sub butCancelEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancelEdit.Click
        '按下butCancelEdit按鈕
        If BeforeCancelEdit() Then
            FillData("")

            MyTableStatus = 0
            SetControls(0)
            AfterCancelEdit()
        End If

        SetButtons()
    End Sub
    '新增
    Private Sub butAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddNew.Click
        '按下butAddNew按鈕
        If BeforeAddNew() Then
            MyTableStatus = 1
            SetControls(1)
            AfterAddNew()
        End If

        SetButtons()
        txtItemStatus.Text = "資料新增中..."
    End Sub
    '複製
    Private Sub butCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCopy.Click
        '按下butAddNew按鈕
        If BeforeAddNew() Then
            MyTableStatus = 1
            SetControls(3)
            AfterAddNew()
        End If

        SetButtons()
        txtItemStatus.Text = "資料複製中..."
    End Sub
    '修改
    Private Sub butEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butEdit.Click
        '按下butEdit按鈕
        If BeforeEdit() Then
            MyTableStatus = 2
            SetControls(2)
            AfterEdit()
        End If

        SetButtons()
        txtItemStatus.Text = "資料修改中..."
    End Sub
    '刪除
    Private Sub butDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butDelete.Click
        Dim msgRet As Integer

        '按下butDelete按鈕
        If BeforeDelete() Then
            msgRet = MsgBox("您是否確定將資料完全刪除!", MsgBoxStyle.YesNo, "注意")

            If msgRet = MsgBoxResult.Yes Then
                '刪除指定資料表的記錄
                DeleteData()

                BeforeLoad()
                FillData("")
                AfterLoad()

                MyTableStatus = 0
                SetControls(0)
                AfterDelete()
            End If
        End If

        SetButtons()
    End Sub

    '尋找
    Private Sub butFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butFind.Click
        FindWindows()
    End Sub
    '查詢
    Private Sub butSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSearch.Click
        '找出指定資料表的記錄
        BeforeLoad()
        FillData(INI_Read("BASIC", "SEARCH", "SQL"))
        AfterLoad()

        MyTableStatus = 0
        AfterScroll()
    End Sub
    '列印
    Private Sub butPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butPrint.Click
        PrintWindows()
    End Sub
#End Region

#Region "共用底層副程式"
#Region "設定狀態及屬性"
    '設定所有按鍵狀態
    Private Sub SetButtons()
        'FStatus:
        ' 0:瀏覽模式
        ' 1:新增模式
        ' 2:修改模式
        txtItemStatus.Text = "Ready"
        butExit.Enabled = (FStatus = 0)

        butFirst.Enabled = (FStatus = 0)
        butPrior.Enabled = (FStatus = 0)
        butNext.Enabled = (FStatus = 0)
        butLast.Enabled = (FStatus = 0)

        butSave.Enabled = ((FStatus = 1) Or (FStatus = 2))
        butCancelEdit.Enabled = ((FStatus = 1) Or (FStatus = 2))
        butAddNew.Enabled = (FStatus = 0) And (AllowAddNew)
        butCopy.Enabled = (FStatus = 0) And (AllowAddNew)
        butEdit.Enabled = (FStatus = 0) And (AllowEdit)
        butDelete.Enabled = (FStatus = 0) And (AllowDelete)

        butFind.Enabled = (FStatus = 0)
        butSearch.Enabled = (FStatus = 0)
        butPrint.Enabled = (FStatus = 0)
        butPrint.Visible = True

    End Sub

    '取得或設定資料表狀態的屬性
    Public Property MyTableStatus() As Byte
        Get
            MyTableStatus = FStatus
        End Get

        Set(ByVal value As Byte)
            FStatus = value
        End Set
    End Property

    '取得或設定允許列印表單的屬性
    Public Property AllowPrint() As Boolean
        Get
            AllowPrint = FAllowPrint
        End Get

        Set(ByVal value As Boolean)
            FAllowPrint = value
            SetButtons()
        End Set
    End Property

    '取得或設定允許新增記錄的屬性
    Public Property AllowAddNew() As Boolean
        Get
            AllowAddNew = FAllowAddNew
        End Get

        Set(ByVal value As Boolean)
            FAllowAddNew = value
            SetButtons()
        End Set
    End Property

    '取得或設定允許修改記錄的屬性
    Public Property AllowEdit() As Boolean
        Get
            AllowEdit = FAllowEdit
        End Get

        Set(ByVal value As Boolean)
            FAllowEdit = value
            SetButtons()
        End Set
    End Property

    '取得或設定允許刪除記錄的屬性
    Public Property AllowDelete() As Boolean
        Get
            AllowDelete = FAllowDelete
        End Get

        Set(ByVal value As Boolean)
            FAllowDelete = value
            SetButtons()
        End Set
    End Property
#End Region

#Region "建立虛擬方法"
    '執行[FillData()]之前
    Public Overridable Sub BeforeLoad()
        '虛擬方法，供繼承者實作
        '載入資料之前(執行FillData方法之前)
    End Sub
    '執行[FillData()]之後
    Public Overridable Sub AfterLoad()
        '虛擬方法，供繼承者實作
        '載入資料之後(執行FillData方法之後)
    End Sub

    '執行[移動記錄]之前
    Public Overridable Function BeforeScroll() As Boolean
        '虛擬方法，供繼承者實作，預設值為True
        '移動記錄之前
        Return True
    End Function
    '執行[移動記錄]之後
    Public Overridable Sub AfterScroll()
        '虛擬方法，供繼承者實作
        '移動記錄之後
    End Sub

    '在使用者按下[還原]之前
    Public Overridable Function BeforeCancelEdit() As Boolean
        '虛擬方法，供繼承者實作，預設值為True
        '執行MyBS物件的CancelEdit方法之前
        Return True
    End Function
    '在使用者按下[還原]之後
    Public Overridable Sub AfterCancelEdit()
        '虛擬方法，供繼承者實作
        '執行MyBS物件的CancelEdit()方法之後
    End Sub

    '在使用者按下[新增]之前
    Public Overridable Function BeforeAddNew() As Boolean
        '虛擬方法，供繼承者實作，預設值為True
        '按butAddNew按鈕之前
        Return True
    End Function
    '在使用者按下[新增]之後
    Public Overridable Sub AfterAddNew()
        '虛擬方法，供繼承者實作
        '按butAddNew按鈕之後
    End Sub

    '在使用者按下[修改]之前
    Public Overridable Function BeforeEdit() As Boolean
        '虛擬方法，供繼承者實作, 預設值為True
        '按butEdit按鈕之前
        Return True
    End Function
    '在使用者按下[修改]之後
    Public Overridable Sub AfterEdit()
        '虛擬方法，供繼承者實作
        '按了butEdit按鈕之後
    End Sub

    '儲存資料之後，執行[EndEdit()]之前
    Public Overridable Function BeforeEndEdit() As Boolean
        '虛擬方法，供繼承者實作，預設值為True
        '儲存資料之後(執行EndEdit方法之前)
        Return True
    End Function
    '儲存資料之後，執行[EndEdit()]之後
    Public Overridable Sub AfterEndEdit()
        '虛擬方法，供繼承者實作
        '儲存資料之後(執行EndEdit方法之後)
    End Sub

    '執行[DeleteData()]之前
    Public Overridable Function BeforeDelete() As Boolean
        '虛擬方法，供繼承者實作，預設值為True
        '刪除記錄前要處理的事情
        Return True
    End Function
    '執行[DeleteData()]之後
    Public Overridable Sub AfterDelete()
        '虛擬方法，供繼承者實作
        '刪除記錄後要處理的事情
    End Sub
#End Region
#End Region

#Region "物件控制處理"
    '尋找視窗
    Public Overridable Sub FindWindows()

    End Sub
    '列印視窗
    Public Overridable Sub PrintWindows()

    End Sub
    '系統載入資料
    Public Overridable Sub FillData(ByVal strSearch As String)
        '虛擬方法，供繼承者實作
        '載入資料
    End Sub

    '系統新增資料
    Public Overridable Function InsertData() As Boolean
        '虛擬方法，供繼承者實作，預設值為True
        '用來執行新增記錄的程式碼
        Return True
    End Function

    '系統更新資料
    Public Overridable Function UpdateData() As Boolean
        '虛擬方法，供繼承者實作，預設值為True
        '用來執行更新記錄的程式碼
        Return True
    End Function

    '系統刪除資料
    Public Overridable Sub DeleteData()
        '虛擬方法，供繼承者實作，預設值為True
        '用來執行刪除記錄的程式碼
    End Sub

    '列印表單
    Public Overridable Sub DoPrint()
        '虛擬方法，供繼承者實作
        '用於執行列印表單的任務
    End Sub

    '輸入控制項的 ReadOnly 屬性及 Enabled 屬性
    Public Overridable Sub SetControls(ByVal butStatus As Integer)
        'butStatus:
        ' 0:一般模式
        ' 1:新增模式
        ' 2:修改模式

        '虛擬方法，供繼承者實作
        '設定所有輸入物件的ReadOnly或Enabled屬性
    End Sub

    '移動DataGridView指標
    Public Overridable Sub FlagMoveSeat(ByVal strStatus As Integer)
        'strStatus:
        ' 0:隨機點選
        ' 1:第一筆
        ' 2:上一筆
        ' 3:下一筆
        ' 4:最未筆
    End Sub
#End Region
End Class