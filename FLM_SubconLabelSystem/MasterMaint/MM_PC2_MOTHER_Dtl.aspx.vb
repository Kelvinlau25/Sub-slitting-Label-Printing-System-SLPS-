
Partial Class MasterMaint_MM_PC2_MOTHER_Dtl
    Inherits Control.Base

    Public Sub New()
        MyBase.SetupKey = "MM_PC2_MOTHER"
    End Sub

    Public Overrides Sub BindData()

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        rfPC2.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "PC2")
        rfThickness.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Thickness")
        rfType.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Type")
        rfWidth.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Width")
        rfLength.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Length")
        rfPackCode.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Packing Code")
        rfGrade.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Grade")
        rfMachine.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Machine")
        rfUnitWeight.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Unit Weight")
        rfNumPack.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "No. Per Pack")

    End Sub

    Protected Sub UCAction_DisplayMode() Handles UCAction.DisplayMode
        Cdisplay.Visible = True
        Cmodify.Visible = False

        Dim _datatable As Data.DataTable = Library.Database.BLL.PC2Mother.GetData(MyBase.Key)

        lblPC2.Text = _datatable.Rows(0)("PC2M").ToString
        lblThickness.Text = _datatable.Rows(0)("THICKNESS").ToString
        lblType.Text = _datatable.Rows(0)("TYPE").ToString
        lblWidth.Text = _datatable.Rows(0)("WIDTH").ToString
        lblLength.Text = _datatable.Rows(0)("LENGTH").ToString
        lblPackCode.Text = _datatable.Rows(0)("PACK_CODE").ToString
        lblGrade.Text = _datatable.Rows(0)("GRADE").ToString
        lblCoreCode.Text = _datatable.Rows(0)("CORE_CODE").ToString
        lblMachine.Text = _datatable.Rows(0)("MACHINE").ToString
        lblUnitWeight.Text = _datatable.Rows(0)("UNIT_WEIGHT").ToString
        lblNumPerPack.Text = _datatable.Rows(0)("NUM_PER_PACK").ToString
        lblRemarks.Text = _datatable.Rows(0)("REMARKS").ToString


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

    End Sub

    Protected Sub UCAction_ModifyMode() Handles UCAction.ModifyMode

        Cdisplay.Visible = False
        Cmodify.Visible = True


        If Not IsPostBack Then

            If MyBase.Action = EnumAction.Edit Then
                txtPC2.Enabled = False
                Dim _datatable As Data.DataTable = Library.Database.BLL.PC2Mother.GetData(MyBase.Key)

                txtPC2.Text = _datatable.Rows(0)("PC2M").ToString
                txtThickness.Text = _datatable.Rows(0)("THICKNESS").ToString
                txtType.Text = _datatable.Rows(0)("TYPE").ToString
                txtWidth.Text = _datatable.Rows(0)("WIDTH").ToString
                txtLength.Text = _datatable.Rows(0)("LENGTH").ToString
                txtPackCode.Text = _datatable.Rows(0)("PACK_CODE").ToString
                txtGrade.Text = _datatable.Rows(0)("GRADE").ToString
                txtCoreCode.Text = _datatable.Rows(0)("CORE_CODE").ToString
                txtMachine.Text = _datatable.Rows(0)("MACHINE").ToString
                txtUnitWeight.Text = _datatable.Rows(0)("UNIT_WEIGHT").ToString
                txtNumPack.Text = _datatable.Rows(0)("NUM_PER_PACK").ToString
                txtRemarks.Text = _datatable.Rows(0)("REMARKS").ToString

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

            ElseIf MyBase.Action = EnumAction.Add Then
                tblPC2.Visible = False
            End If

        End If

    End Sub

    ''' <summary>
    ''' Handler the add Submit Function
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UCAction_AddAction() Handles UCAction.AddAction, UCAction.EditAction

        txtPC2.Text = txtThickness.Text + "-" + txtType.Text + "-" + txtWidth.Text + "x" + txtLength.Text + _
                      "-" + txtGrade.Text + txtPackCode.Text + txtNumPack.Text + txtCoreCode.Text

        Dim _temp As String = Library.Database.BLL.PC2Mother.Maint(MyBase.Key, txtPC2.Text, txtThickness.Text, txtType.Text, txtWidth.Text, _
                                                                          txtLength.Text, txtPackCode.Text, txtGrade.Text, txtCoreCode.Text, _
                                                                          txtMachine.Text, txtUnitWeight.Text, txtNumPack.Text, txtRemarks.Text, CInt(MyBase.Action))


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

    Protected Sub UCAction_AddResetAction() Handles UCAction.AddResetAction, UCAction.EditResetAction
        Response.Redirect(Request.RawUrl)
    End Sub

    ''' <summary>
    ''' Delete Action
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UCAction_DeleteAction() Handles UCAction.DeleteAction
        Dim _temp As String = Library.Database.BLL.PC2Mother.Maint(MyBase.Key, txtPC2.Text, txtThickness.Text, txtType.Text, txtWidth.Text, _
                                                                          txtLength.Text, txtPackCode.Text, txtGrade.Text, txtCoreCode.Text, _
                                                                          txtMachine.Text, txtUnitWeight.Text, txtNumPack.Text, txtRemarks.Text, CInt(Library.Root.Control.Base.EnumAction.Delete))

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
