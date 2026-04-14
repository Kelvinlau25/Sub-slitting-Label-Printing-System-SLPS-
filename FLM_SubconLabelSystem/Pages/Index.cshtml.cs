using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PFRLabelIssuing.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;

        public IndexModel(IConfiguration configuration)
        {
            _config = configuration;
        }

        [BindProperty]
        public string txtuserID { get; set; } = string.Empty;

        [BindProperty]
        public string txtpassword { get; set; } = string.Empty;

        [BindProperty]
        public string txtCaptcha { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public string PageTitle =>
            _config["AppSettings:title"] ?? "Login";

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

                string aclConnStr = _config.GetConnectionString("ACL") ?? "";
                string systemName = _config["AppSettings:SystemName"] ?? "";

                var user = ValidateUser(aclConnStr, txtuserID.Trim(), txtpassword.Trim(), systemName);

                if (user != null)
                {
                    HttpContext.Session.SetString("USERID", user.UserId);
                    HttpContext.Session.SetString("USERNAME", user.EmpName);
                    HttpContext.Session.SetString("IPAddr",
                        System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0)?.ToString() ?? "");

                    var roleMap = _config.GetSection("AppSettings:RoleToUlevel").GetChildren()
                        .ToDictionary(x => x.Key, x => x.Value ?? "");
                    string ulevel = roleMap.TryGetValue(user.IdAclRole.ToString(), out var mapped) ? mapped : "";
                    HttpContext.Session.SetString("ULEVEL", ulevel);

                    LoadAccessRights(user.UserId, aclConnStr, systemName);
                    HttpContext.Session.SetString("COMPANYCODE", user.CompanyCode);
                    HttpContext.Session.SetString("ABORESSION", user.CompanyCode.Replace("-", ""));
                    HttpContext.Session.SetString("gstrUserID", user.UserId);

                    return Redirect("~/Menu");
                }
                else
                {
                    ErrorMessage = "Invalid User ID or Password. Please try again.";
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

        // =============================================
        // HELPER: Validate User Login via pab_ACL
        // =============================================
        private UserInfo? ValidateUser(string aclConnStr, string userId, string password, string systemName)
        {
            using var conn = new SqlConnection(aclConnStr);
            conn.Open();

            string sql = @"SELECT u.ID_ACL_USER, u.USER_ID, u.USR_PASSWORD, u.EMP_NAME, u.COMPANY, r.ID_ACL_ROLE
                           FROM ACL_User u
                           INNER JOIN ACL_USR_ROLE ur ON ur.ID_ACL_USER = u.ID_ACL_USER
                           INNER JOIN ACL_ROLE r      ON r.ID_ACL_ROLE  = ur.ID_ACL_ROLE
                           INNER JOIN ACL_RESOURCE res ON res.ID_ACL_RESOURCE = r.ID_ACL_RESOURCE
                           WHERE u.USER_ID     = @userId
                           AND   u.STATUS_IND  = @activeStatus
                           AND   res.RESOURCE_NAME = @systemName";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@systemName", systemName);
            cmd.Parameters.AddWithValue("@activeStatus", _config["AppSettings:UserActiveStatus"] ?? "A");

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string hashedPassword = reader["USR_PASSWORD"].ToString() ?? "";

                if (VerifyHashedPassword(hashedPassword, password))
                {
                    return new UserInfo
                    {
                        IdAclUser   = Convert.ToInt32(reader["ID_ACL_USER"]),
                        IdAclRole   = Convert.ToInt32(reader["ID_ACL_ROLE"]),
                        UserId      = reader["USER_ID"].ToString() ?? "",
                        EmpName     = reader["EMP_NAME"].ToString() ?? "",
                        CompanyCode = reader["COMPANY"].ToString() ?? ""
                    };
                }
            }

            return null;
        }

        // =============================================
        // HELPER: Verify Rfc2898 Hashed Password
        // =============================================
        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null) return false;
            if (password == null) throw new ArgumentNullException(nameof(password));

            int hashTotalLength  = int.Parse(_config["AppSettings:HashTotalLength"]  ?? "49");
            int hashVersionByte  = int.Parse(_config["AppSettings:HashVersionByte"]  ?? "0");
            int saltLength       = int.Parse(_config["AppSettings:HashSaltLength"]   ?? "16");
            int subkeyLength     = int.Parse(_config["AppSettings:HashSubkeyLength"] ?? "32");
            int saltOffset       = int.Parse(_config["AppSettings:HashSaltOffset"]   ?? "1");
            int subkeyOffset     = int.Parse(_config["AppSettings:HashSubkeyOffset"] ?? "17");
            int iterationCount   = int.Parse(_config["AppSettings:HashIterationCount"] ?? "1000");

            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != hashTotalLength) || (src[0] != hashVersionByte)) return false;

            byte[] salt = new byte[saltLength];
            byte[] storedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(src, saltOffset,    salt,         0, saltLength);
            Buffer.BlockCopy(src, subkeyOffset,  storedSubkey, 0, subkeyLength);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterationCount, HashAlgorithmName.SHA1);
            byte[] generatedSubkey = pbkdf2.GetBytes(subkeyLength);

            return storedSubkey.SequenceEqual(generatedSubkey);
        }

        // =============================================
        // HELPER: Load ACL access rights into session
        // =============================================
        private void LoadAccessRights(string userId, string aclConnStr, string systemName)
        {
            try
            {
                using var conn = new SqlConnection(aclConnStr);
                conn.Open();

                string sql = @"SELECT ac.VIEW_RIGHT, ac.ADD_RIGHT, ac.EDIT_RIGHT, ac.DELETE_RIGHT
                               FROM ACL_Access_Control ac
                               INNER JOIN ACL_ROLE r      ON r.ID_ACL_ROLE        = ac.ID_ACL_ROLE
                               INNER JOIN ACL_USR_ROLE ur ON ur.ID_ACL_ROLE       = r.ID_ACL_ROLE
                               INNER JOIN ACL_User u      ON u.ID_ACL_USER        = ur.ID_ACL_USER
                               INNER JOIN ACL_RESOURCE res ON res.ID_ACL_RESOURCE = r.ID_ACL_RESOURCE
                               WHERE u.USER_ID          = @userId
                               AND   res.RESOURCE_NAME  = @systemName
                               AND   ac.ID_ACL_RESOURCE = res.ID_ACL_RESOURCE";

                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@systemName", systemName);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    HttpContext.Session.SetString("ACL_CAN_VIEW",   reader["VIEW_RIGHT"].ToString());
                    HttpContext.Session.SetString("ACL_CAN_ADD",    reader["ADD_RIGHT"].ToString());
                    HttpContext.Session.SetString("ACL_CAN_EDIT",   reader["EDIT_RIGHT"].ToString());
                    HttpContext.Session.SetString("ACL_CAN_DELETE", reader["DELETE_RIGHT"].ToString());
                }
                else
                {
                    HttpContext.Session.SetString("ACL_CAN_VIEW",   "FALSE");
                    HttpContext.Session.SetString("ACL_CAN_ADD",    "FALSE");
                    HttpContext.Session.SetString("ACL_CAN_EDIT",   "FALSE");
                    HttpContext.Session.SetString("ACL_CAN_DELETE", "FALSE");
                }
            }
            catch
            {
                HttpContext.Session.SetString("ACL_CAN_VIEW",   "FALSE");
                HttpContext.Session.SetString("ACL_CAN_ADD",    "FALSE");
                HttpContext.Session.SetString("ACL_CAN_EDIT",   "FALSE");
                HttpContext.Session.SetString("ACL_CAN_DELETE", "FALSE");
            }
        }

        private sealed class UserInfo
        {
            public int    IdAclUser   { get; set; }
            public int    IdAclRole   { get; set; }
            public string UserId      { get; set; } = string.Empty;
            public string EmpName     { get; set; } = string.Empty;
            public string CompanyCode { get; set; } = string.Empty;
        }
    }
}
