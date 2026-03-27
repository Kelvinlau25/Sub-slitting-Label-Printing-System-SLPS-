
Partial Class MasterMaint_LabelPlan_Dtl
    Inherits Control.Base

    Public Sub New()

        MyBase.SetupKey = "VIEW_LOT_SLITTING_SERIES"

    End Sub

    Public Overrides Sub BindData()

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub UCAction_DisplayMode() Handles UCAction.DisplayMode
        Cdisplay.Visible = True

        Dim _datatable As Data.DataTable = Library.Database.BLL.LotSlitting.GetData(MyBase.Key)

        lblCompCode.Text = _datatable.Rows(0)("COMPANYCODE").ToString
        lblPlanYrMth.Text = _datatable.Rows(0)("PLAN_YEAR_MONTH").ToString
        lblProdLine.Text = _datatable.Rows(0)("PRODLINE_NO").ToString

        lblPC1Mother.Text = _datatable.Rows(0)("PC1_MOTHER").ToString
        lblPC2Mother.Text = _datatable.Rows(0)("PC2_MOTHER").ToString
        lblUnitWeightMother.Text = _datatable.Rows(0)("M_UNITWEIGHT").ToString

        lblPC1Customer.Text = _datatable.Rows(0)("PC1_CUST").ToString
        lblPC2Customer.Text = _datatable.Rows(0)("PC2_CUST").ToString
        lblUnitWeightCustomer.Text = _datatable.Rows(0)("C_UNITWEIGHT").ToString

        lblLotNo.Text = _datatable.Rows(0)("LOTNO").ToString
        lblLotSlitNo.Text = _datatable.Rows(0)("SLIT_LOT_NO").ToString
        lblStatus.Text = _datatable.Rows(0)("STATUS").ToString

        UCAction.CreatedBy = ""
        UCAction.CreatedDate = New DateTime()
        UCAction.CreatedLoc = ""

        Dim tmpCreateDate As DateTime

        If (DateTime.TryParse(_datatable.Rows(0)("CREATED_DATE").ToString, tmpCreateDate)) Then
            UCAction.CreatedBy = _datatable.Rows(0)("CREATED_BY").ToString
            UCAction.CreatedDate = tmpCreateDate.ToString("dd / MMM / yyyy hh:mm:ss")
            UCAction.CreatedLoc = _datatable.Rows(0)("CREATED_LOC").ToString
        End If

        UCAction.UpdatedBy = ""
        UCAction.UpdatedDate = New DateTime()
        UCAction.UpdatedLoc = ""

        Dim tmpUpdateDate As DateTime

        If (DateTime.TryParse(_datatable.Rows(0)("UPDATED_DATE").ToString, tmpUpdateDate)) Then
            UCAction.UpdatedBy = _datatable.Rows(0)("UPDATED_BY").ToString
            UCAction.UpdatedDate = tmpUpdateDate.ToString("dd / MMM / yyyy hh:mm:ss")
            UCAction.UpdatedLoc = _datatable.Rows(0)("UPDATED_LOC").ToString
        End If

        UCAction.EditMode = _datatable.Rows(0)("REC_TYPE") <> "5"

        If MyBase.Action = EnumAction.Delete Then
            Dim btnSubmit As Button = DirectCast(UCAction.FindControl("btnSubmit"), Button)
            btnSubmit.Visible = True
        End If

        If MyBase.Action = EnumAction.View Then
            Dim btnSubmit As Button = DirectCast(UCAction.FindControl("btnSubmit"), Button)
            Dim btnDelete As Button = DirectCast(UCAction.FindControl("btnDelete"), Button)
            btnSubmit.Visible = False
            If Session("ULEVEL") = 2 Then
                btnDelete.Visible = False
            Else
                btnDelete.Visible = True
            End If
        End If

        Dim hpLink As HyperLink = DirectCast(UCAction.FindControl("hpLink"), HyperLink)
        hpLink.Visible = False

    End Sub

    Protected Sub UCAction_AddResetAction() Handles UCAction.AddResetAction, UCAction.EditResetAction

        Response.Redirect(Request.RawUrl)

    End Sub

    ''' <summary>
    ''' Delete Action
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UCAction_DeleteAction() Handles UCAction.DeleteAction

        Dim _temp As String = Library.Database.BLL.LotSlitting.Maint(MyBase.Key, lblLotNo.Text, "", "", "", _
                                                                          "", "", "", "", _
                                                                          "", "", "", "", CInt(Library.Root.Control.Base.EnumAction.Delete))
        If _temp = "1" Then
            Response.Redirect(MyBase.GetUrl(EnumAction.None), False)
        Else
            If _temp = "0" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, MyBase.Action.ToString))
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class
