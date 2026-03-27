Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Net
Imports System.Diagnostics
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Transactions_SlitSeries
    Inherits Control.Base
    Dim _list As Library.Database.ListCollection
    Dim v_Status As String
    Dim Lot_NO As String
    Dim _datatable As Data.DataTable
    Dim v_ID_PC2_LOTNO As Integer
    Dim Company_Code As String
    Dim User_Level As Integer
    Dim User_ID As String
    Dim z As Integer
    Dim a_Redirect As String
    Dim b_LotNo As String

    Private str_MSSQL_Connstr As String = ConfigurationManager.ConnectionStrings("PFR_Label_DB").ConnectionString

    Public Sub New()

        MyBase.SetupKey = "PC2_LOTNO"
        MyBase.DefaultSort = "ID_PC2_LOTNO"
        MyBase.SortDirection = 0
        MyBase.GridViewCheckColumn = False
        MyBase.PrintControl = False
        MyBase.DeleteControl = True
        MyBase.GridViewRadioColumn = False
        MyBase.ViewHistoryControl = False
        MyBase.RecordTypeColumn = 11

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        MyBase.GridView = grdResult

        If Session("ULEVEL") = 2 Then
            MyBase.DeleteControl = False
        Else
            MyBase.DeleteControl = True
        End If

        If Not Request.QueryString("action") Is Nothing Then

            If Not Request.QueryString("ID") Is Nothing Then

                If Request.QueryString("action").ToString.ToLower.Equals("create") Then
                    Create_Lot_Slittting(Request.QueryString("ID").ToString.Trim)
                End If

            End If

        End If

    End Sub

    Public Overrides Sub BindData()

        Company_Code = Session("COMPANYCODE")
        User_Level = Session("ULEVEL")

        If Session("ULEVEL") = 3 Then
            '_list = Library.Database.BLL.SlitSeries.List("SlitSeries_func(" + "'" + Company_Code + "'" + ")", "ID_PC2_LOTNO", MyBase.SearchField, MyBase.SearchValue, "UPDATED_DATE", 1, MyBase.PageNo, MyBase.ShowDeleted)
            _list = Library.Database.BLL.SlitSeries.List("|" + Company_Code, "ID_PC2_LOTNO", MyBase.SearchField, MyBase.SearchValue, "UPDATED_DATE", 1, MyBase.PageNo, MyBase.ShowDeleted)
        Else
            _list = Library.Database.BLL.SlitSeries.List("PV_PC2_LOTNO", "ID_PC2_LOTNO", MyBase.SearchField, MyBase.SearchValue, "UPDATED_DATE", 1, MyBase.PageNo, MyBase.ShowDeleted)
        End If

        Dim _obj_DT As DataTable = _list.Data
        Dim _obj_dr() As DataRow

        _obj_DT.Columns.Add("Create_URL", GetType(String))

        _obj_dr = _obj_DT.Select("STATUS = 'Create'")

        If _obj_dr.GetLength(0) > 0 Then

            For _int_iCreate As Integer = 0 To (_obj_dr.GetLength(0) - 1)

                _obj_dr(_int_iCreate)("Create_URL") = String.Format("SLIT_SERIES.aspx?action={0}&ID={1}", "create", _obj_dr(_int_iCreate)("ID_PC2_LOTNO").ToString().Trim)

            Next

        End If

        grdResult.DataSource = _list.Data
        grdResult.DataBind()

        UCFooter.TotalRecords = _list.TotalRow

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then

            If hdn_LotID.Value.Equals("") = False Then

                Lot_NO = hdn_LotID.Value

            End If

        End If

    End Sub

    Public Sub Create_Lot_Slittting(ByVal Lot As String)

        Company_Code = Session("COMPANYCODE")
        User_Level = Session("ULEVEL")

        Dim _datatable As Data.DataTable = Library.Database.BLL.SlitSeries.GetData(Lot.ToString)

        'Dim typeofslit = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(0)
        'Dim matrixpos = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(1)
        'Dim matrixinc = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(2)

        If _datatable.Rows(0)("LOT_STATUS") = "Create" Then
            Dim v_TYPE_OF_SLIT As Integer = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(0)
            Dim v_MATRIX_POS = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(1)
            Dim v_MATRIX_INC = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(2)
            Dim v_LOTNO As String = _datatable.Rows(0)("LOTNO").ToString
            Dim v_NO_OF_SLIT As Integer = _datatable.Rows(0)("NO_OF_SLIT")
            Dim v_CompanyCode As String = _datatable.Rows(0)("COMPANYTO")
            User_ID = Session("USERID")
            Session("LOTNO") = _datatable.Rows(0)("LOTNO").ToString

            Dim upd_stat As String = ""
            If User_Level = 1 Then
                upd_stat = Library.Database.BLL.SlitSeries.CreateSlitRec(v_CompanyCode, Lot.ToString, v_TYPE_OF_SLIT, v_MATRIX_POS, v_MATRIX_INC, v_LOTNO, v_NO_OF_SLIT, User_ID)
            End If

            If User_Level = 2 And Company_Code.Equals(v_CompanyCode) = True Then
                upd_stat = Library.Database.BLL.SlitSeries.CreateSlitRec(Company_Code, Lot.ToString, v_TYPE_OF_SLIT, v_MATRIX_POS, v_MATRIX_INC, v_LOTNO, v_NO_OF_SLIT, User_ID)
            Else
                If User_Level = 2 And Company_Code.Equals(v_CompanyCode) = False Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Creating Lot Slitting records for other company is not allowed")
                End If
            End If

            If User_Level = 3 Then
                upd_stat = Library.Database.BLL.SlitSeries.CreateSlitRec(Company_Code, Lot.ToString, v_TYPE_OF_SLIT, v_MATRIX_POS, v_MATRIX_INC, v_LOTNO, v_NO_OF_SLIT, User_ID)
            End If

            If upd_stat.Equals("1") = False Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Creating Slitting records is not successful")
            Else
                Dim url As String = "~/MasterMaint/LabelPlan.aspx?itm1=" & v_LOTNO.ToString
                'Dim url As String = "~/MasterMaint/LabelPlan.aspx"
                Response.Redirect(url)
            End If
        Else
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This Lot No Slitting is already completed")
        End If

    End Sub

    Public Sub Redirect_Label_Plan()

        Response.Redirect("~/MasterMaint/LabelPlan.aspx")

    End Sub

End Class
