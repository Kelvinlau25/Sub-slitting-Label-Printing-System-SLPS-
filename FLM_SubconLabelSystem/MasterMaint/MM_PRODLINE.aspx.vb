Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Net
Imports System.Diagnostics

Partial Class MasterMaint_MM_PRODLINE

    Inherits Control.Base
    Dim _list As Library.Database.ListCollection
    Private str_MSSQL_Connstr As String = ConfigurationManager.ConnectionStrings("PFR_Label_DB").ConnectionString

    Public Sub New()

        MyBase.SetupKey = "MM_PRODLINE"
        MyBase.DefaultSort = "ID_MM_PRODLINE"
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
        
        _list = Library.Database.BLL.MM_PRODLINE.List("PV_MM_PRODLINE", "ID_MM_PRODLINE", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)

        grdResult.DataSource = _list.Data
        grdResult.DataBind()
        UCFooter.TotalRecords = _list.TotalRow

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
        End If
    End Sub

End Class
