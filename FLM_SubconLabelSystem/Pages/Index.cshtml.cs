using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace PFRLabelIssuing.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string txtuserID { get; set; } = string.Empty;

        [BindProperty]
        public string txtpassword { get; set; } = string.Empty;

        [BindProperty]
        public string txtCaptcha { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public string PageTitle =>
            _configuration["AppSettings:title"] ?? "Login";

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPostLogin()
        {
            try
            {
                // Validate captcha
                var sessionCaptcha = HttpContext.Session.GetString("randomStr");
                if (string.IsNullOrEmpty(sessionCaptcha) ||
                    !sessionCaptcha.ToUpper().Equals((txtCaptcha ?? "").ToUpper().Trim()))
                {
                    ErrorMessage = "Please enter the correct Captcha.";
                    txtpassword = string.Empty;
                    txtCaptcha = string.Empty;
                    return Page();
                }

                if (string.IsNullOrWhiteSpace(txtuserID))
                {
                    ErrorMessage = "Please enter User ID to Login";
                    return Page();
                }

                if (string.IsNullOrWhiteSpace(txtpassword))
                {
                    ErrorMessage = "Please enter a password to Login";
                    return Page();
                }

                string[] _arr_str_password = Library.Database.BLL.USER_LOGIN.UserLogin(txtuserID.Trim(), "", 0);

                if (!_arr_str_password[0].Equals("") && _arr_str_password[6].Equals(""))
                {
                    string cipherpass = _arr_str_password[0];
                    string retriveIV = GlobalFunctions.RetrieveIV(cipherpass);
                    string inputcipherpass = GlobalFunctions.EncryptIV(txtpassword.Trim(), retriveIV);

                    string[] _arr_str_login = Library.Database.BLL.USER_LOGIN.UserLogin(txtuserID.Trim(), inputcipherpass, 1);

                    if (!_arr_str_login[0].Equals("") && _arr_str_login[6].Equals(""))
                    {
                        HttpContext.Session.SetString("USERID", _arr_str_login[0]);
                        HttpContext.Session.SetString("ULEVEL", _arr_str_login[1]);
                        HttpContext.Session.SetString("COMPANYCODE", _arr_str_login[2]);
                        HttpContext.Session.SetString("USERNAME", _arr_str_login[3]);
                        HttpContext.Session.SetString("IPAddr",
                            System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0).ToString());

                        if (txtpassword.Trim().Length < 9 ||
                            !Regex.IsMatch(txtpassword.Trim(), @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$"))
                        {
                            HttpContext.Session.SetString("pswlenerr", "1");
                        }

                        if (!_arr_str_login[5].Equals(""))
                        {
                            DateTime pwd_date = Convert.ToDateTime(_arr_str_login[5]);
                            TimeSpan pwd_life = DateTime.Now.Subtract(pwd_date);
                            int maxAge = Convert.ToInt32(
                                _configuration["AppSettings:Max_Password_Age"] ?? "90");

                            if ((maxAge - pwd_life.Days) <= 0)
                            {
                                HttpContext.Session.SetString("pswexpired", "1");
                            }
                        }

                        return Redirect("~/Menu");
                    }
                    else
                    {
                        ErrorMessage = _arr_str_login[6];
                        txtuserID = string.Empty;
                        txtpassword = string.Empty;
                        txtCaptcha = string.Empty;
                        return Page();
                    }
                }
                else
                {
                    ErrorMessage = _arr_str_password[6];
                    txtuserID = string.Empty;
                    txtpassword = string.Empty;
                    txtCaptcha = string.Empty;
                    return Page();
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Invalid User ID Or Password. Please try again.";
                txtCaptcha = string.Empty;
                return Page();
            }
        }

        public IActionResult OnPostClear()
        {
            txtuserID = string.Empty;
            txtpassword = string.Empty;
            txtCaptcha = string.Empty;
            return Page();
        }
    }
}
