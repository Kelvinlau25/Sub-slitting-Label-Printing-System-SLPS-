Imports System.Data
Partial Class MasterMaint_MM_PC1_Dtl
    Inherits Control.Base

    Public Sub New()
        MyBase.SetupKey = "MM_PC1" '
    End Sub

    Public Overrides Sub BindData()

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        rfPC1.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "PC1")
        rfNameDelivery.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Description")

    End Sub

    Protected Sub UCAction_DisplayMode() Handles UCAction.DisplayMode
        Cdisplay.Visible = True
        Cmodify.Visible = False

        Dim _datatable As Data.DataTable = Library.Database.BLL.PC1.GetData(MyBase.Key)
        lblPC1.Text = _datatable.Rows(0)("PC1").ToString
        lblNameDelivery.Text = _datatable.Rows(0)("DESCRIPTION").ToString.ToUpper

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
                txtPC1.Enabled = False
                lbPC1.Visible = True
                txtPC1.Visible = False

                Dim _datatable As Data.DataTable = Library.Database.BLL.PC1.GetData(MyBase.Key)

                txtPC1.Text = _datatable.Rows(0)("PC1").ToString
                lbPC1.Text = _datatable.Rows(0)("PC1").ToString


                txtNameDelivery.Text = _datatable.Rows(0)("DESCRIPTION").ToString.ToUpper


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
                    lbPC1.Visible = False
                    txtPC1.Visible = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handler the add Submit Function
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UCAction_AddAction() Handles UCAction.AddAction, UCAction.EditAction

        Dim _temp As String = ""

        If MyBase.Action = EnumAction.Edit Then
            _temp = Library.Database.BLL.PC1.Maint(MyBase.Key, txtPC1.Text, "0", txtNameDelivery.Text, CInt(MyBase.Action))
        ElseIf MyBase.Action = EnumAction.Add Then
            _temp = Library.Database.BLL.PC1.Maint("0", txtPC1.Text, "0", txtNameDelivery.Text, CInt(MyBase.Action))
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
        Dim _temp As String = Library.Database.BLL.PC1.Maint(MyBase.Key, lblPC1.Text, "0", lblNameDelivery.Text, _
                                                                                CInt(Library.Root.Control.Base.EnumAction.Delete))

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
