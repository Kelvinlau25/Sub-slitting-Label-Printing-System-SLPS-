Imports System.Data
Partial Class MasterMaint_MM_COMPANY_Dtl
    Inherits Control.Base

    Public Sub New()
        MyBase.SetupKey = "MM_COMPANY"
    End Sub

    Public Overrides Sub BindData()

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        rfCompCode.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Company Code")
        rfCompName.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Company Name")
        rfAddress.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Address")
        rfTelephone.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Telephone")
        rfEmail.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Email Address")
        rfSlit.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Slit Code")
        rfEmail2.ErrorMessage = "Email Address format:   eg.you@(domain.com)"
    End Sub

    Protected Sub UCAction_DisplayMode() Handles UCAction.DisplayMode
        Cdisplay.Visible = True
        Cmodify.Visible = False

        Dim _datatable As Data.DataTable = Library.Database.BLL.Company.GetData(MyBase.Key)
        lblCompCode.Text = _datatable.Rows(0)("COMPANYCODE").ToString
        lblCompName.Text = _datatable.Rows(0)("COMPANYNAME").ToString.ToUpper
        lblAddress.Text = _datatable.Rows(0)("ADDRESS").ToString.ToUpper
        lblTelephone.Text = _datatable.Rows(0)("TELEPHONE").ToString
        lblEmail.Text = _datatable.Rows(0)("Email").ToString
        lblslit.Text = _datatable.Rows(0)("SLIT_CODE").ToString

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
                txtCompCode.Visible = False
                lbCompCode.Visible = True
                Dim _datatable As Data.DataTable = Library.Database.BLL.Company.GetData(MyBase.Key)
                txtCompCode.Text = _datatable.Rows(0)("COMPANYCODE").ToString
                lbCompCode.Text = _datatable.Rows(0)("COMPANYCODE").ToString
                txtCompName.Text = _datatable.Rows(0)("COMPANYNAME").ToString
                txtAddress.Text = _datatable.Rows(0)("ADDRESS").ToString
                txtTelephone.Text = _datatable.Rows(0)("TELEPHONE").ToString
                txtEmail.Text = _datatable.Rows(0)("EMAIL").ToString
                txtSlit.Text = _datatable.Rows(0)("SLIT_CODE").ToString
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

            Else
                If MyBase.Action = EnumAction.Add Then
                    lbCompCode.Visible = False
                    txtCompCode.Visible = True
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' Handler the add Submit Function
    ''' </summary>
    ''' <remarks></remarks>
         Protected Sub UCAction_AddAction() Handles UCAction.AddAction, UCAction.EditAction
        '          Dim Slit As Integer
        '          Int32.TryParse(txtSlit.Text, Slit)
        'Dim txtsli = Slit.ToString("D2")

        Dim _temp As String = ""
        Dim slit As String = txtSlit.Text.Trim

                  If MyBase.Action = EnumAction.Edit Then
            _temp = Library.Database.BLL.Company.Maint(MyBase.Key, txtCompCode.Text.Trim, txtCompName.Text.Trim.ToUpper, slit, txtAddress.Text.Trim.ToUpper, txtTelephone.Text.Trim, _
                                                                                   txtEmail.Text.Trim, CInt(MyBase.Action))
                  ElseIf MyBase.Action = EnumAction.Add Then
            _temp = Library.Database.BLL.Company.Maint("0", txtCompCode.Text.Trim, txtCompName.Text.Trim.ToUpper, slit, txtAddress.Text.Trim.ToUpper, txtTelephone.Text.Trim, _
                                                                                   txtEmail.Text.Trim, CInt(MyBase.Action))
                  End If



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
                  Dim _temp As String = Library.Database.BLL.Company.Maint(MyBase.Key, lblCompCode.Text, lblCompName.Text, "", lblAddress.Text, lblTelephone.Text, _
                                                                                lblEmail.Text, CInt(Library.Root.Control.Base.EnumAction.Delete))

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
