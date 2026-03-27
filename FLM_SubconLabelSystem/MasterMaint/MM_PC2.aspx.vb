
Partial Class MasterMaint_MM_PC2
    Inherits Control.Base
    Dim _list As Library.Database.ListCollection
    Private str_MSSQL_Connstr As String = ConfigurationManager.ConnectionStrings("PFR_Label_DB").ConnectionString

    Public Sub New()
        MyBase.SetupKey = "MM_PC2"
        MyBase.DefaultSort = "ID_MM_PC2"
        MyBase.SortDirection = 0
        MyBase.GridViewCheckColumn = False
        MyBase.PrintControl = False
        MyBase.DeleteControl = True
        MyBase.GridViewRadioColumn = False
        MyBase.ViewHistoryControl = True
        MyBase.RecordTypeColumn = 6
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        MyBase.GridView = grdResult

        If Session("ULEVEL") = 3 Then
            MyBase.DeleteControl = False
        Else
            MyBase.DeleteControl = True
        End If
    End Sub

    Public Overrides Sub BindData()
        _list = Library.Database.BLL.PC2.List("PV_MM_PC2", "ID_MM_PC2", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)

        grdResult.DataSource = _list.Data
        grdResult.DataBind()

        UCFooter.TotalRecords = _list.TotalRow
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
        End If
    End Sub

End Class
