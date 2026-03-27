Imports System.Data
Partial Class PopUp_PP_PC2
    Inherits Control.Base
    Dim _list As Library.Database.ListCollection

    Protected pc2mother As String = String.Empty
    Protected lblpc2mother As String = String.Empty
    Protected refno As String = String.Empty
    Protected str_BtnName As String = ""
    Protected str_hdn_PC2_Mother As String = ""
    Protected str_hdn_Unit_Weight_Mother As String = ""
 
    Public Sub New()

        MyBase.SetupKey = "PP_PC2"
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

        Dim refno As String = 0
        Dim _str_ProdLine As String = 0
        Dim _str_PC1Mother As String = 0

        str_BtnName = ""
        str_hdn_PC2_Mother = ""
        str_hdn_Unit_Weight_Mother = ""

        If Not Request.QueryString("itm3") Is Nothing Then
            If Request.QueryString("itm3").ToString <> "" Then
                refno = Request.QueryString("itm3").ToString
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please select the reference no.")
                Exit Sub
            End If
        End If

        If Not Request.QueryString("itm4") Is Nothing Then
            If Request.QueryString("itm4").ToString <> "" And Request.QueryString("itm4").ToString <> "," Then
                _str_ProdLine = Request.QueryString("itm4").ToString
                _str_ProdLine = _str_ProdLine.Replace(",", "")
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please select the Production Line.")

                Exit Sub
            End If
        End If

        If Not Request.QueryString("itm5") Is Nothing Then
            If Request.QueryString("itm5").ToString <> "" And Request.QueryString("itm5").ToString <> "," Then
                _str_PC1Mother = Request.QueryString("itm5").ToString
                _str_PC1Mother = _str_PC1Mother.Replace(",", "")
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please select the PC 1 Mother.")

                Exit Sub
            End If
        End If

        If Not Request.QueryString("itm6") Is Nothing Then
            If Request.QueryString("itm6").ToString <> "" And Request.QueryString("itm6").ToString <> "," Then
                str_BtnName = Request.QueryString("itm6").ToString
                str_BtnName = str_BtnName.Replace(",", "")

            End If
        End If

        If Not Request.QueryString("itm7") Is Nothing Then
            If Request.QueryString("itm7").ToString <> "" And Request.QueryString("itm7").ToString <> "," Then
                str_hdn_PC2_Mother = Request.QueryString("itm7").ToString
                str_hdn_PC2_Mother = str_hdn_PC2_Mother.Replace(",", "")

            End If
        End If

        If Not Request.QueryString("itm8") Is Nothing Then
            If Request.QueryString("itm8").ToString <> "" And Request.QueryString("itm8").ToString <> "," Then
                str_hdn_Unit_Weight_Mother = Request.QueryString("itm8").ToString
                str_hdn_Unit_Weight_Mother = str_hdn_Unit_Weight_Mother.Replace(",", "")

            End If
        End If


        refno = " REFNO = '" & refno & "' AND PRODLINE_NO = '" & _str_ProdLine & "' AND PC1_MOTHER = '" & _str_PC1Mother & "'"
        _list = Library.Database.BLL.PC1.List2(refno, "PV_MM_PC2_POPUPv1", "ID_MM_PC2", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)
        grdResult.DataSource = _list.Data
        grdResult.DataBind()

        UCFooter.TotalRecords = _list.TotalRow

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        pc2mother = MyBase.Item1
        lblpc2mother = MyBase.Item2
        refno = MyBase.Item3

    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Dim ddl = DirectCast(UCSearch.FindControl("ddlSearch"), DropDownList)
        ddl.SelectedIndex = 1

    End Sub


End Class
