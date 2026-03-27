
Partial Class Transactions_SUBSLITREQSEARCH

    Inherits Control.Base
    Dim _list As Library.Database.ListCollection
    Private str_MSSQL_Connstr As String = ConfigurationManager.ConnectionStrings("PFR_Label_DB").ConnectionString

    Public Sub New()

        MyBase.SetupKey = "PV_SUBSLIT_REQUEST_LIST"
        MyBase.DefaultSort = "UPDATED_DATE"
        MyBase.SortDirection = 1
        MyBase.GridViewCheckColumn = False
        MyBase.PrintControl = False
        MyBase.DeleteControl = True
        MyBase.GridViewRadioColumn = False
        MyBase.ViewHistoryControl = False
        MyBase.RecordTypeColumn = 7

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        MyBase.GridView = grdResult

        If Session("ULEVEL") = 3 Then
            MyBase.DeleteControl = False
            Add_Button.Visible = False
        Else
            MyBase.DeleteControl = True
        End If

    End Sub

    Public Overrides Sub BindData()

        Dim Company_Code = Session("COMPANYCODE")

        If Session("ULEVEL") = 3 Then
            _list = Library.Database.BLL.user.List("MM_SUBSLIT_func(" + "'" + Company_Code + "'" + ")", "UPDATED_DATE", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)

        Else
            _list = Library.Database.BLL.user.List("PV_SUBSLIT_REQUEST_LIST", "UPDATED_DATE", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)
        End If

        grdResult.DataSource = _list.Data
        grdResult.DataBind()

        UCFooter.TotalRecords = _list.TotalRow
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
        End If
    End Sub

    Protected Sub grdResult_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdResult.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(grdResult, "Select$" & e.Row.RowIndex)
        End If


    End Sub

    Protected Sub grdResult_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdResult.SelectedIndexChanged

        Dim v_Refno As String = String.Empty
        Dim v_ID_SSR As String = String.Empty
        Dim v_Req_Status As String = String.Empty

        If Session("ULEVEL") = 3 Then
            v_Refno = grdResult.SelectedRow.Cells(8).Text
            v_ID_SSR = grdResult.SelectedRow.Cells(9).Text
            v_Req_Status = grdResult.SelectedRow.Cells(10).Text
        Else
            v_Refno = grdResult.SelectedRow.Cells(9).Text
            v_ID_SSR = grdResult.SelectedRow.Cells(10).Text
            v_Req_Status = grdResult.SelectedRow.Cells(11).Text
        End If

        If Session("ULEVEL") = 3 And v_Req_Status <> "Submitted" And v_Req_Status <> "Cancel" Then
            Exit Sub
        End If

        If v_Req_Status = "New" Then
            Dim url As String = "~/Transactions/SUBSLIT_REQ_.aspx?itm1=" & v_Refno.ToString & "&itm2= " & v_ID_SSR.ToString
            Response.Redirect(url)
        Else
            Dim url As String = "~/Transactions/SSR_SEARCH_Dtl.aspx?itm1=" & v_Refno.ToString & "&itm2= " & v_ID_SSR.ToString
            Response.Redirect(url)
        End If



    End Sub


    Protected Sub SSRRefNo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        MsgBox(grdResult.SelectedRow.Cells(8).Text & "9: " & grdResult.SelectedRow.Cells(9).Text)

    End Sub

    Protected Sub Add_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Add_Button.Click

        Dim url As String = "~/Transactions/SUBSLIT_REQ_.aspx"
        Response.Redirect(url)

    End Sub

End Class
