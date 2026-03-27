Imports System.Data
Partial Class PopUp_PP_PC2_Cust
    Inherits Control.Base
    Dim _list As Library.Database.ListCollection

    Protected pc2cust As String = String.Empty
    Protected lblpc2cust As String = String.Empty
    Protected refno As String = String.Empty
    Protected str_hdn_PC2Customer As String = ""
    Protected str_hdn_UnitWeightCustomer As String = ""
    Protected str_BtnName As String = ""

    Public Sub New()
        MyBase.SetupKey = "PP_PC2_Cust"
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

        Dim refno As String = "0"
        Dim _str_ProdLine As String = "0"
        Dim _str_PC1Mother As String = "0"
        Dim _str_PC2Mother As String = "0"
        Dim _str_PC1Customer As String = "0"


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
                _str_PC2Mother = Request.QueryString("itm6").ToString
                _str_PC2Mother = _str_PC2Mother.Replace(",", "")
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please select the PC 2 Mother.")
                Exit Sub
            End If
        End If

        If Not Request.QueryString("itm7") Is Nothing Then
            If Request.QueryString("itm7").ToString <> "" And Request.QueryString("itm7").ToString <> "," Then
                _str_PC1Customer = Request.QueryString("itm7").ToString
                _str_PC1Customer = _str_PC1Customer.Replace(",", "")
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please select the PC 1 Customer.")
                Exit Sub
            End If
        End If

        If Not Request.QueryString("itm8") Is Nothing Then
            If Request.QueryString("itm8").ToString <> "" And Request.QueryString("itm8").ToString <> "," Then
                str_BtnName = Request.QueryString("itm8").ToString
                str_BtnName = str_BtnName.Replace(",", "")
            End If
        End If

        If Not Request.QueryString("itm9") Is Nothing Then
            If Request.QueryString("itm9").ToString <> "" And Request.QueryString("itm9").ToString <> "," Then
                str_hdn_PC2Customer = Request.QueryString("itm9").ToString
                str_hdn_PC2Customer = str_hdn_PC2Customer.Replace(",", "")
            End If
        End If

        If Not Request.QueryString("itm10") Is Nothing Then
            If Request.QueryString("itm10").ToString <> "" And Request.QueryString("itm10").ToString <> "," Then
                str_hdn_UnitWeightCustomer = Request.QueryString("itm10").ToString
                str_hdn_UnitWeightCustomer = str_hdn_UnitWeightCustomer.Replace(",", "")
            End If
        End If


        refno = " REFNO = '" & refno & "' AND PC1_MOTHER = '" & _str_PC1Mother & "' AND PC2_MOTHER = '" & _str_PC2Mother & "' AND PC1_CUST = '" & _str_PC1Customer & "' AND PRODLINE_NO = '" & _str_ProdLine & "'"


        _list = Library.Database.BLL.PC1.List2(refno, "PV_MM_PC2CUST_POPUPv1", "ID_MM_PC2", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)
        grdResult.DataSource = _list.Data
        grdResult.DataBind()

        UCFooter.TotalRecords = _list.TotalRow

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pc2cust = MyBase.Item1
        lblpc2cust = MyBase.Item2
        refno = MyBase.Item3

    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim ddl = DirectCast(UCSearch.FindControl("ddlSearch"), DropDownList)
        ddl.SelectedIndex = 1

    End Sub

End Class
