using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePassword : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.Title = ConfigurationManager.AppSettings["SystemName"].ToString();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        rfpassword.ErrorMessage = string.Format("{0} Cannot be Empty!", "Password");
        repassword.ErrorMessage = "Invalid Password Format.";
        repassword.ValidationExpression = @"^[a-zA-Z0-9!@#$%&*]{0,15}$";

        rfnewpassword.ErrorMessage = string.Format("{0} Cannot be Empty!", "New Password");
        renewpassword.ValidationExpression = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$";
        renewpassword.ErrorMessage = "Invalid Format. New Password Must Contain at least 1 Alphabet and 1 Number with a Minimum 9 Characters.";

        rfconfirmpassword.ErrorMessage = string.Format("{0} Cannot be Empty!", "Confirmation Password");
        reconfirmpassword.ValidationExpression = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$";
        reconfirmpassword.ErrorMessage = "Invalid Format. Confirm Password Must Contain at least 1 Alphabet and 1 Number with a Minimum 9 Characters.";

        nePassword.ErrorMessage = "New Password must not be the same as Current Password";
        cvPassword.ErrorMessage = "New Password Does Not Match With the Confirmation Password";
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        txtpassword.Text = string.Empty;
        txtnewpassword.Text = string.Empty;
        txtconfirmpassword.Text = string.Empty;
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string userID = Session["USERID"].ToString();
        string cipherpass = string.Empty;
        string retriveIV = string.Empty;
        string inputcipherpass = string.Empty;
        int duplicatepass = 0;

        string[] _arr_str_password = Library.Database.BLL.USER_LOGIN.UserLogin(userID, "", 0);
        cipherpass = _arr_str_password[0];
        retriveIV = GlobalFunctions.RetrieveIV(cipherpass);
        inputcipherpass = GlobalFunctions.EncryptIV(txtpassword.Text.Trim(), retriveIV);

        string[] _arr_str_prevpass = Library.Database.BLL.CHANGE_PASSWORD.retrieve_pass_arr(userID);

        if (!_arr_str_prevpass[0].Equals("") || !_arr_str_prevpass[0].Equals("0"))
        {
            foreach (string prevpass in _arr_str_prevpass)
            {
                if (txtnewpassword.Text.Trim().Equals(GlobalFunctions.Decrypt(prevpass)))
                {
                    duplicatepass++;
                }
            }
        }

        string updateStatus = Library.Database.BLL.CHANGE_PASSWORD.chg_password(userID, inputcipherpass, GlobalFunctions.Encrypt(txtnewpassword.Text.Trim()), duplicatepass);

        if (updateStatus == "1")
        {
            Session.Remove("pswexpired");
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "New Password Changed Successfully");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "RedirectScript", "window.parent.location = '../Default.aspx'", true);
        }
        else if (updateStatus == "2")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Cannot Reuse the Last 5 Passwords. Please Enter a New Password.");
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Invalid Password");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        var pswexpired = Session["pswexpired"];
        if (pswexpired != null && pswexpired.ToString() == "1")
        {
            return;
        }
        else
        {
            Response.End();
        }
    }

    private void addErrorIntoValidationSummary(string errorMessage)
    {
        CustomValidator custVal = new CustomValidator();
        custVal.IsValid = false;
        custVal.ErrorMessage = errorMessage;
        custVal.EnableClientScript = false;
        custVal.Display = ValidatorDisplay.None;
        custVal.ValidationGroup = "login";
        Page.Form.Controls.Add(custVal);
    }
}