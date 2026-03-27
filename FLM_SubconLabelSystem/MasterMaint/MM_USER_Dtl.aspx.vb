
Partial Class MasterMaint_MM_USER_Dtl
    Inherits Control.Base

    Public Sub New()
        MyBase.SetupKey = "MM_USER"
    End Sub

    Public Overrides Sub BindData()

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        rfName.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Name")
        rfUserID.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "UserID")
        rfDepartment.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Department")
        rfEmail.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Email Address")
        rfPassword.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Password")
        rfCompName.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Company Name")
        rePassword.ValidationExpression = "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$"
        rePassword.ErrorMessage = "Invalid Format. New Password Must Contain at least 1 Alphabet and 1 Number with a Minimum 9 Characters."
        rfEmail2.ErrorMessage = "Email Address format:   eg.you@(domain.com)"

        Dim _dtCompName As Data.DataTable = Library.Database.BLL.user.GetDLLData("CompanyName", "")

        If _dtCompName.Rows.Count > 0 Then
            ddlCompName.Items.Insert(0, New ListItem(" - SELECT - ", ""))
            ddlCompName.DataSource = _dtCompName
            ddlCompName.DataTextField = "CompanyName"
            ddlCompName.DataValueField = "ID_MM_COMPANY"
            ddlCompName.DataBind()
        ElseIf _dtCompName.Rows.Count = 0 Then
            ddlCompName.Items.Add(New ListItem(" - SELECT - ", ""))
        End If

        If Session("ULEVEL") = 3 Or Session("ULEVEL") = 2 Then
            resetlnk.Visible = False
        Else
            resetlnk.Visible = True
        End If

    End Sub

    Protected Sub UCAction_DisplayMode() Handles UCAction.DisplayMode

        Cdisplay.Visible = True
        Cmodify.Visible = False

        Dim _datatable As Data.DataTable = Library.Database.BLL.user.GetData(MyBase.Key)

        lblCompName.Text = _datatable.Rows(0)("COMPANYNAME").ToString
        lblName.Text = _datatable.Rows(0)("NAME").ToString
        lblUserID.Text = _datatable.Rows(0)("USERID").ToString
        lblDepartment.Text = _datatable.Rows(0)("DEPARTMENT").ToString
        lblEmail.Text = _datatable.Rows(0)("Email").ToString


        If _datatable.Rows(0)("STATUS") = 0 Then
            lblaccstats.Text = "Normal"
        ElseIf _datatable.Rows(0)("STATUS") = 1 Then
            lblaccstats.Text = "Locked"
        End If

        If _datatable.Rows(0)("ULEVEL") = 1 Then
            lblLevel.Text = "System Administrator"
        Else
            If _datatable.Rows(0)("ULEVEL") = 2 Then
                lblLevel.Text = "User"
            Else
                lblLevel.Text = "Vendor"
            End If
        End If

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

    Public Sub CheckRadioButton()
        If RBLevel1.Checked = False And RBLevel2.Checked = False And RBLevel3.Checked = False Then
            rfRB.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "User Level")
        End If
    End Sub

    Protected Sub UCAction_ModifyMode() Handles UCAction.ModifyMode

        If MyBase.Action = EnumAction.Add Or MyBase.Action = EnumAction.Edit Then
            CheckRadioButton()
        End If

        Cdisplay.Visible = False
        Cmodify.Visible = True


        If Not IsPostBack Then

            If MyBase.Action = EnumAction.Edit Then
                tblpassword.Visible = False
                txtUserID.Visible = False
                lbUserID.Visible = True

                If Session("ULEVEL") = 2 Then
                    lbCompName.Visible = True
                    ddlCompName.Visible = False
                Else
                    lbCompName.Visible = False
                    ddlCompName.Visible = True
                End If

                If Session("ULEVEL") = 1 Then
                    lblAccStats2.Visible = False
                    ddlAccStats.Visible = True
                Else
                    lblAccStats2.Visible = True
                    ddlAccStats.Visible = False
                End If

                Dim _datatable As Data.DataTable = Library.Database.BLL.user.GetData(MyBase.Key)

                ddlCompName.SelectedValue = _datatable.Rows(0)("ID_MM_COMPANY")
                lbCompName.Text = _datatable.Rows(0)("COMPANYNAME")
                txtName.Text = _datatable.Rows(0)("NAME").ToString
                txtUserID.Text = _datatable.Rows(0)("USERID").ToString
                lbUserID.Text = _datatable.Rows(0)("USERID").ToString
                txtDepartment.Text = _datatable.Rows(0)("DEPARTMENT").ToString
                txtEmail.Text = _datatable.Rows(0)("EMAIL").ToString

                If _datatable.Rows(0)("STATUS") = 0 Then
                    lblAccStats2.Text = "Normal"
                    ddlAccStats.SelectedValue = _datatable.Rows(0)("STATUS")
                ElseIf _datatable.Rows(0)("STATUS") = 1 Then
                    lblAccStats2.Text = "Locked"
                    ddlAccStats.SelectedValue = _datatable.Rows(0)("STATUS")
                End If

                If _datatable.Rows(0)("ULEVEL") = 1 Then
                    RBLevel1.Checked = True
                    TextBox1.Text = "1"
                End If
                If _datatable.Rows(0)("ULEVEL") = 2 Then
                    RBLevel2.Checked = True
                    TextBox1.Text = "2"
                End If
                If _datatable.Rows(0)("ULEVEL") = 3 Then
                    RBLevel3.Checked = True
                    TextBox1.Text = "3"
                End If

                txtPassword.Text = _datatable.Rows(0)("PASSWORD").ToString

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
                    tblpassword.Visible = True
                    txtUserID.Visible = True
                    lbUserID.Visible = False
                    resetlnk.Visible = False
                    lblAccStats2.Visible = True
                    ddlAccStats.Visible = False
                    lblAccStats2.Text = "Normal"
                    ddlAccStats.SelectedValue = 0
                    If Session("ULEVEL") = "2" Then
                        lbCompName.Visible = True
                        lbCompName.Text = Session("COMPANYCODE")
                        ddlCompName.Visible = False
                        RBLevel1.Enabled = False
                    Else
                        lbCompName.Visible = False
                        ddlCompName.Visible = True
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handler the add Submit Function
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UCAction_AddAction() Handles UCAction.AddAction, UCAction.EditAction

        If RBLevel1.Checked = False And RBLevel2.Checked = False And RBLevel3.Checked = False Then
            MsgBox("User Level Must be checked")
        Else
            Dim _temp As String = Library.Database.BLL.user.Maint(MyBase.Key, ddlCompName.SelectedValue, txtName.Text, txtUserID.Text, txtDepartment.Text, txtEmail.Text, _
                                                                                   RBLevel1.Checked, RBLevel2.Checked, RBLevel3.Checked, GlobalFunctions.encrypt(txtPassword.Text), ddlAccStats.SelectedValue, CInt(MyBase.Action))

            If _temp = "1" Then
                Response.Redirect(MyBase.GetUrl(EnumAction.None), False)
            Else
                If _temp = "0" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, MyBase.Action.ToString))
                Else
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
                End If
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

        Dim _temp As String = Library.Database.BLL.user.Maint(MyBase.Key, "0", lblName.Text, lblUserID.Text, lblDepartment.Text, lblEmail.Text, _
                                                                                RBLevel1.Checked, RBLevel2.Checked, RBLevel3.Checked, txtPassword.Text, ddlAccStats.SelectedValue, CInt(Library.Root.Control.Base.EnumAction.Delete))

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

    Protected Sub RBLevel1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBLevel1.CheckedChanged
        RBLevel2.Checked = False
        RBLevel3.Checked = False
        TextBox1.Text = "1"
    End Sub

    Protected Sub RBLevel2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBLevel2.CheckedChanged
        RBLevel1.Checked = False
        RBLevel3.Checked = False
        TextBox1.Text = "2"
    End Sub

    Protected Sub RBLevel3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBLevel3.CheckedChanged
        RBLevel1.Checked = False
        RBLevel2.Checked = False
        TextBox1.Text = "3"
    End Sub

    Protected Sub ddlCompName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompName.SelectedIndexChanged

    End Sub

    Protected Sub resetlnk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles resetlnk.Click

        Dim _temp As String = Library.Database.BLL.user.ResetPass(MyBase.Key, "00YToB6QsF8IHDg0ts+HSw==XXrRKhkbJuN4oft7xknZmg==", 3)

        If _temp = "1" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Your password has been reset successfully to abcd12345. Please proceed to change your password immediately.');window.location ='MM_USER.aspx';", True)
        Else
            If _temp = "0" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, MyBase.Action.ToString))
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
            End If
        End If

    End Sub


End Class
