Imports System.Data
Partial Class PopUp_PP_PC2SUB
    Inherits Control.Base
    Dim _list As Library.Database.ListCollection

    Protected pc2mother As String = String.Empty
    Protected lblpc2mother As String = String.Empty

    Public Sub New()
        MyBase.SetupKey = "PP_PC2SUB"
        MyBase.DefaultSort = "ID_MM_PC2"
        MyBase.SortDirection = 0
        MyBase.GridViewCheckColumn = False
        MyBase.PrintControl = False
        MyBase.DeleteControl = False
        MyBase.GridViewRadioColumn = False
        MyBase.ViewHistoryControl = False
        MyBase.RecordTypeColumn = False

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Public Overrides Sub BindData()

        If Request.QueryString("itm1") = "pc2mother" Then
            lblTittle.Text = "PC2 Mother"
        Else
            lblTittle.Text = "PC2 Child"
        End If
        If MyBase.SearchField <> "" Then

            _list = Library.Database.BLL.PC1.List("PV_MM_PC2SUB_POPUP", "ID_MM_PC2", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)
            grdResult.DataSource = _list.Data
            grdResult.DataBind()

            UCFooter.TotalRecords = _list.TotalRow

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        pc2mother = MyBase.Item1
        lblpc2mother = MyBase.Item2

    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Dim ddl = DirectCast(UCSearch.FindControl("ddlSearch"), DropDownList)
        ddl.SelectedIndex = 1

    End Sub

End Class
