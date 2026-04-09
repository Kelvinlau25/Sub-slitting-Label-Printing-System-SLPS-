using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Response.Redirect("~/MasterMaint/MM_COMPANY.aspx");
        }
    }

    protected void cusCustom_ServerValidate(object sender, ServerValidateEventArgs e)
    {
        try
        {
            if (!Session["randomStr"].ToString().ToUpper().Equals(txtCaptcha.Text.ToUpper().Trim()))
            {
                e.IsValid = false;
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please enter the correct Captcha.");
                txtpassword.Text = string.Empty;
                txtCaptcha.Text = string.Empty;
                return;
            }

            if (txtuserID.Text.Trim() == string.Empty)
            {
                e.IsValid = false;
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please enter User ID to Login");
            }
            else
            {
                if (txtpassword.Text.Trim() == string.Empty)
                {
                    e.IsValid = false;
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please enter a password to Login");
                }
                else
                {
                    string[] _arr_str_password = Library.Database.BLL.USER_LOGIN.UserLogin(txtuserID.Text.Trim(), "", 0);
                    string cipherpass = string.Empty;
                    string retriveIV = string.Empty;
                    string inputcipherpass = string.Empty;

                    if (!_arr_str_password[0].Equals("") && _arr_str_password[6].Equals(""))
                    {
                        cipherpass = _arr_str_password[0];
                        retriveIV = GlobalFunctions.RetrieveIV(cipherpass);
                        inputcipherpass = GlobalFunctions.EncryptIV(txtpassword.Text.Trim(), retriveIV);

                        string[] _arr_str_login = Library.Database.BLL.USER_LOGIN.UserLogin(txtuserID.Text.Trim(), inputcipherpass, 1);

                        if (!_arr_str_login[0].Equals("") && _arr_str_login[6].Equals(""))
                        {
                            Session["USERID"] = _arr_str_login[0];
                            Session["ULEVEL"] = _arr_str_login[1];
                            Session["COMPANYCODE"] = _arr_str_login[2];
                            Session["USERNAME"] = _arr_str_login[3];
                            Session["IPAddr"] = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0).ToString();

                            if (txtpassword.Text.Trim().Length < 9 || !Regex.IsMatch(txtpassword.Text.Trim(), @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$"))
                            {
                                Session["pswlenerr"] = 1;
                            }

                            if (!_arr_str_login[5].Equals(""))
                            {
                                DateTime pwd_date = Convert.ToDateTime(_arr_str_login[5]);
                                TimeSpan pwd_life = DateTime.Now.Subtract(pwd_date);

                                if ((Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Max_Password_Age"]) - pwd_life.Days) <= 0)
                                {
                                    Session["pswexpired"] = 1;
                                }

                                Response.Redirect("~/Menu.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/Menu.aspx");
                            }
                        }
                        else
                        {
                            var error_msg = _arr_str_login[6];
                            e.IsValid = false;
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, error_msg);
                            txtuserID.Text = string.Empty;
                            txtpassword.Text = string.Empty;
                            txtCaptcha.Text = string.Empty;
                        }
                    }
                    else
                    {
                        var error_msg = _arr_str_password[6];
                        e.IsValid = false;
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, error_msg);
                        txtuserID.Text = string.Empty;
                        txtpassword.Text = string.Empty;
                        txtCaptcha.Text = string.Empty;
                    }
                }
            }
        }
        catch (Exception)
        {
            e.IsValid = false;
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Invalid User ID Or Password. Please try again.");
            txtCaptcha.Text = string.Empty;
        }
    }

    protected void ClearButton_Click(object sender, EventArgs e)
    {
        txtuserID.Text = string.Empty;
        txtpassword.Text = string.Empty;
        txtCaptcha.Text = string.Empty;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Abandon();
            Session.Clear();
        }

        txtuserID.Focus();

        //// -- ByPass ACL
        //Response.Redirect("~/mRawMaterial/Home.aspx");
    }
}