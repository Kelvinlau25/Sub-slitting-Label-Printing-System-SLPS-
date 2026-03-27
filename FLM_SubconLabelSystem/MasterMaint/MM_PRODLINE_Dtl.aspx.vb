
Partial Class MasterMaint_MM_PRODLINE_Dtl

    Inherits Control.Base

    Public Sub New()

        MyBase.SetupKey = "MM_PRODLINE"

    End Sub

    Public Overrides Sub BindData()

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        rfProdLine.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "PRODLINE_NO")

    End Sub

    Protected Sub UCAction_DisplayMode() Handles UCAction.DisplayMode

        Cdisplay.Visible = True
        Cmodify.Visible = False

        Dim _datatable As Data.DataTable = Library.Database.BLL.MM_PRODLINE.GetData(MyBase.Key)

        lblProdLine.Text = _datatable.Rows(0)("PRODLINE_NO").ToString
        lblDesc.Text = _datatable.Rows(0)("DESCRIPTION").ToString

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
                txtProdLine.Visible = False
                lbProdLine.Visible = True

                Dim _datatable As Data.DataTable = Library.Database.BLL.MM_PRODLINE.GetData(MyBase.Key)

                txtProdLine.Text = _datatable.Rows(0)("PRODLINE_NO").ToString
                lbProdLine.Text = _datatable.Rows(0)("PRODLINE_NO").ToString
                txtDesc.Text = _datatable.Rows(0)("DESCRIPTION").ToString

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
                txtProdLine.Visible = True
                lbProdLine.Visible = False
            End If
        End If

    End Sub

    ''' <summary>
    ''' Handler the add Submit Function
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UCAction_AddAction() Handles UCAction.AddAction, UCAction.EditAction

        Dim _temp As String = Library.Database.BLL.MM_PRODLINE.Maint(MyBase.Key, txtProdLine.Text, txtDesc.Text, _
                                                                        CInt(MyBase.Action))

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

        Dim _temp As String = Library.Database.BLL.MM_PRODLINE.Maint(MyBase.Key, txtProdLine.Text, txtDesc.Text, _
                                                                         Int(Library.Root.Control.Base.EnumAction.Delete))

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
