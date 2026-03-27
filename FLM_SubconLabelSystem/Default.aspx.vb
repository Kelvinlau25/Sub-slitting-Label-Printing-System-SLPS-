Imports System
Imports System.IO
Imports System.Collections


Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoginButton.Click
        If Page.IsValid Then
            Response.Redirect("~/MasterMaint/MM_COMPANY.aspx")
        End If
    End Sub

    Protected Sub cusCustom_ServerValidate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        Try
            If Session("randomStr").ToString.ToUpper.Equals(txtCaptcha.Text.ToUpper.Trim) = False Then
                e.IsValid = False
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please enter the correct Captcha.")
                txtpassword.Text = String.Empty
                txtCaptcha.Text = String.Empty
                Exit Sub
            End If

            If txtuserID.Text.Trim = String.Empty Then
                e.IsValid = False
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please enter User ID to Login")
            Else
                If txtpassword.Text.Trim = String.Empty Then
                    e.IsValid = False
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please enter a password to Login")
                Else

                    Dim _arr_str_password() As String = Library.Database.BLL.USER_LOGIN.UserLogin(txtuserID.Text.Trim, "", 0)
                    Dim cipherpass = String.Empty
                    Dim retriveIV = String.Empty
                    Dim inputcipherpass = String.Empty

                    If _arr_str_password(0).Equals("") = False And _arr_str_password(6).Equals("") = True Then
                        cipherpass = _arr_str_password(0)
                        retriveIV = GlobalFunctions.retriveIV(cipherpass)
                        inputcipherpass = GlobalFunctions.encryptIV(txtpassword.Text.Trim, retriveIV)

                        Dim _arr_str_login() As String = Library.Database.BLL.USER_LOGIN.UserLogin(txtuserID.Text.Trim, inputcipherpass, 1)

                        If _arr_str_login(0).Equals("") = False And _arr_str_login(6).Equals("") = True Then

                            Session("USERID") = _arr_str_login(0)
                            Session("ULEVEL") = _arr_str_login(1)
                            Session("COMPANYCODE") = _arr_str_login(2)
                            Session("USERNAME") = _arr_str_login(3)
                            Session("IPAddr") = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0).ToString()

                            If (Len(txtpassword.Text.Trim) < 9 Or Regex.IsMatch(txtpassword.Text.Trim, "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$") = False) Then
                                Session("pswlenerr") = 1
                            End If

                            If (_arr_str_login(5).Equals("") = False) Then

                                Dim pwd_date As DateTime = _arr_str_login(5)
                                Dim pwd_life As TimeSpan

                                pwd_life = DateTime.Now.Subtract(pwd_date)

                                If ((Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Max_Password_Age")) - pwd_life.Days) <= 0) Then
                                    Session("pswexpired") = 1
                                End If

                                Response.Redirect("~/Menu.aspx")

                            Else

                                Response.Redirect("~/Menu.aspx")

                            End If
                        Else
                            Dim error_msg = _arr_str_login(6)
                            e.IsValid = False
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, error_msg)
                            txtuserID.Text = String.Empty
                            txtpassword.Text = String.Empty
                            txtCaptcha.Text = String.Empty
                        End If
                    Else
                        Dim error_msg = _arr_str_password(6)
                        e.IsValid = False
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, error_msg)
                        txtuserID.Text = String.Empty
                        txtpassword.Text = String.Empty
                        txtCaptcha.Text = String.Empty

                    End If
                End If
            End If

        Catch ex As Exception

            e.IsValid = False
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Invalid User ID Or Password. Please try again.")
            txtCaptcha.Text = String.Empty
        End Try

    End Sub

    Protected Sub ClearButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearButton.Click
        txtuserID.Text = String.Empty
        txtpassword.Text = String.Empty
        txtCaptcha.Text = String.Empty
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session.Abandon()
            Session.Clear()
        End If

        txtuserID.Focus()

        '' ''-- ByPass ACL
        ''Response.Redirect("~/mRawMaterial/Home.aspx")
    End Sub
End Class