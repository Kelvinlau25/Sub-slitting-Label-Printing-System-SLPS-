
Partial Class MasterMaint_MM_COMPANY
    Inherits Control.Base
    Dim _list As Library.Database.ListCollection
    Private str_MSSQL_Connstr As String = ConfigurationManager.ConnectionStrings("PFR_Label_DB").ConnectionString

    Public Sub New()
        MyBase.SetupKey = "MM_COMPANY"
        MyBase.DefaultSort = "ID_MM_COMPANY"
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

        If Session("ULEVEL") = 3 Or Session("ULEVEL") = 2 Then
            MyBase.DeleteControl = False
        Else
            MyBase.DeleteControl = True
        End If

    End Sub

    Public Overrides Sub BindData()

        If Session("ULEVEL") = 3 Or Session("ULEVEL") = 2 Then
            _list = Library.Database.BLL.Company.List("MM_COMPANY_func(" + "'" + Session("COMPANYCODE") + "'" + ")", "ID_MM_COMPANY", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)
        Else
            _list = Library.Database.BLL.Company.List("PV_MM_COMPANY", "ID_MM_COMPANY", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)
        End If

        grdResult.DataSource = _list.Data
        grdResult.DataBind()

        UCFooter.TotalRecords = _list.TotalRow
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim _obj_WIP_Process As New WIP_Process.cls_WIP_Process(str_MSSQL_Connstr, "", "", "", "")
            '_obj_WIP_Process.AddRFID_Usage("", "", "", "Master Maintenance", "User Authorization Maintenance", "PC", Session("GroupNo"), Session("gstrUserID"), System.Web.HttpContext.Current.Request.UserHostAddress.ToString)
            '_obj_WIP_Process.AddRFID_Usage("", "", "", "Master Maintenance", "User Authorization Maintenance", "PC", Session("GroupNo"), Session("USERID"), System.Web.HttpContext.Current.Request.UserHostAddress.ToString)
        End If
    End Sub


End Class
