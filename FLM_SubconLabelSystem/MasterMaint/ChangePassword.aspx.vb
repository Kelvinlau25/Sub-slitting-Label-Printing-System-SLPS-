
Partial Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Session("id") = "62502"

        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        Me.Title = ConfigurationManager.AppSettings("SystemName").ToString

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        rfpassword.ErrorMessage = String.Format("{0} Cannot be Empty!", "Password")
        repassword.ErrorMessage = "Invalid Password Format."
        repassword.ValidationExpression = "^[a-zA-Z0-9!@#$%&*]{0,15}$"
        rfnewpassword.ErrorMessage = String.Format("{0} Cannot be Empty!", "New Password")
        renewpassword.ValidationExpression = "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$"
        renewpassword.ErrorMessage = "Invalid Format. New Password Must Contain at least 1 Alphabet and 1 Number with a Minimum 9 Characters."
        rfconfirmpassword.ErrorMessage = String.Format("{0} Cannot be Empty!", "Confirmation Password")
        reconfirmpassword.ValidationExpression = "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$"
        reconfirmpassword.ErrorMessage = "Invalid Format. Confirm Password Must Contain at least 1 Alphabet and 1 Number with a Minimum 9 Characters."
        nePassword.ErrorMessage = "New Password must not be the same as Current Password"
        cvPassword.ErrorMessage = "New Password Does Not Match With the Confirmation Password"

        '^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$ Minimum 9 characters at least 1 Alphabet and 1 Number with Optional Special Chars
        '^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{9,15}$ Minimum 9 characters at least 1 Alphabet and 1 Number 

    End Sub

    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click

        txtpassword.Text = String.Empty
        txtnewpassword.Text = String.Empty
        txtconfirmpassword.Text = String.Empty

    End Sub

    Protected Sub btnupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdate.Click


        Dim userID As String = Session("USERID")
        Dim cipherpass = String.Empty
        Dim retriveIV = String.Empty
        Dim inputcipherpass = String.Empty
        Dim duplicatepass = 0
        Dim _arr_str_password() As String = Library.Database.BLL.USER_LOGIN.UserLogin(userID.ToString, "", 0)
        cipherpass = _arr_str_password(0)
        retriveIV = GlobalFunctions.retriveIV(cipherpass)
        inputcipherpass = GlobalFunctions.encryptIV(txtpassword.Text.Trim, retriveIV)

        Dim _arr_str_prevpass() As String = Library.Database.BLL.CHANGE_PASSWORD.retrieve_pass_arr(userID.ToString)

        If _arr_str_prevpass(0).Equals("") = False Or _arr_str_prevpass(0).Equals(0) = False Then
            For Each prevpass As String In _arr_str_prevpass
                If txtnewpassword.Text.Trim.Equals(GlobalFunctions.decrypt(prevpass)) Then
                    duplicatepass += 1
                End If
            Next
        End If

        Dim Update_Status As String = Library.Database.BLL.CHANGE_PASSWORD.chg_password(userID.ToString, inputcipherpass, GlobalFunctions.encrypt(txtnewpassword.Text.Trim), duplicatepass)

            If Update_Status = "1" Then
                Session.Remove("pswexpired")
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "New Password Changed Successfully")
                'Response.Redirect("~/Default.aspx")
                Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "RedirectScript", "window.parent.location = '../Default.aspx'", True)
                'txtuserID.Text = String.Empty
                'txtpassword.Text = String.Empty
                'txtnewpassword.Text = String.Empty
                'txtconfirmpassword.Text = String.Empty
            ElseIf Update_Status = "2" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Cannot Reuse the Last 5 Passwords. Please Enter a New Password.")
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Invalid Password")
            End If

    End Sub

    ''' <summary>
    '''  this is bind the error message into the validation summary
    ''' </summary>
    ''' <param name="ErrorMessage"></param>
    ''' <remarks></remarks>

    Private Sub addErrorIntoValidationSummary(ByVal ErrorMessage As String)

        Dim custVal As New CustomValidator
        custVal.IsValid = False
        custVal.ErrorMessage = ErrorMessage
        custVal.EnableClientScript = False
        custVal.Display = ValidatorDisplay.None
        custVal.ValidationGroup = "login"
        Page.Form.Controls.Add(custVal)

    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Dim pswexpired = Session("pswexpired")
        If pswexpired = 1 Then
            Exit Sub
        Else
            Response.End()
        End If

    End Sub
End Class