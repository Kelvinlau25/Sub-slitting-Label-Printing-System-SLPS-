using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ChangePasswordModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string NewPassword { get; set; } = string.Empty;

        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string StartupScript { get; set; } = string.Empty;

        public int MaxPasswordAge => Convert.ToInt32(_configuration["AppSettings:Max_Password_Age"] ?? "90");

        public bool IsPswExpired =>
            HttpContext.Session.GetString("pswexpired") == "1";

        public void OnGet()
        {
        }

        public IActionResult OnPostUpdate()
        {
            // Client-side-like validations
            if (string.IsNullOrWhiteSpace(Password))
            {
                StartupScript = "alert('Password Cannot be Empty!');";
                return Page();
            }
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                StartupScript = "alert('New Password Cannot be Empty!');";
                return Page();
            }
            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                StartupScript = "alert('Confirmation Password Cannot be Empty!');";
                return Page();
            }
            if (Password == NewPassword)
            {
                StartupScript = "alert('New Password must not be the same as Current Password');";
                return Page();
            }
            if (NewPassword != ConfirmPassword)
            {
                StartupScript = "alert('New Password Does Not Match With the Confirmation Password');";
                return Page();
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(NewPassword, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$"))
            {
                StartupScript = "alert('Invalid Format. New Password Must Contain at least 1 Alphabet and 1 Number with a Minimum 9 Characters.');";
                return Page();
            }

            string userID = HttpContext.Session.GetString("USERID") ?? string.Empty;
            string cipherpass = string.Empty;
            string retriveIV = string.Empty;
            string inputcipherpass = string.Empty;
            int duplicatepass = 0;

            string[] _arr_str_password = Library.Database.BLL.USER_LOGIN.UserLogin(userID, "", 0);
            cipherpass = _arr_str_password[0];
            retriveIV = GlobalFunctions.RetrieveIV(cipherpass);
            inputcipherpass = GlobalFunctions.EncryptIV(Password.Trim(), retriveIV);

            string[] _arr_str_prevpass = Library.Database.BLL.CHANGE_PASSWORD.retrieve_pass_arr(userID);

            if (!_arr_str_prevpass[0].Equals("") || !_arr_str_prevpass[0].Equals("0"))
            {
                foreach (string prevpass in _arr_str_prevpass)
                {
                    if (NewPassword.Trim().Equals(GlobalFunctions.Decrypt(prevpass)))
                    {
                        duplicatepass++;
                    }
                }
            }

            string updateStatus = Library.Database.BLL.CHANGE_PASSWORD.chg_password(userID, inputcipherpass, GlobalFunctions.Encrypt(NewPassword.Trim()), duplicatepass);

            if (updateStatus == "1")
            {
                HttpContext.Session.Remove("pswexpired");
                StartupScript = "alert('New Password Changed Successfully'); window.parent.location = '/';";
                return Page();
            }
            else if (updateStatus == "2")
            {
                StartupScript = "alert('Cannot Reuse the Last 5 Passwords. Please Enter a New Password.');";
                return Page();
            }
            else
            {
                StartupScript = "alert('Invalid Password');";
                return Page();
            }
        }

        public IActionResult OnPostReset()
        {
            Password = string.Empty;
            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;
            return Page();
        }

        public IActionResult OnPostCancel()
        {
            if (HttpContext.Session.GetString("pswexpired") == "1")
            {
                return Page();
            }
            return new EmptyResult();
        }
    }
}
