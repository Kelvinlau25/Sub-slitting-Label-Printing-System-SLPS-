using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_USER_DtlModel : BasePageModel
    {
        public MM_USER_DtlModel()
        {
            SetupKey = "MM_USER";
            FunctionControl = false;
        }

        // Display fields
        public string DisplayCompanyName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string DisplayUserID { get; set; } = string.Empty;
        public string DisplayDepartment { get; set; } = string.Empty;
        public string DisplayEmail { get; set; } = string.Empty;
        public string DisplayLevel { get; set; } = string.Empty;
        public string DisplayAccStatus { get; set; } = string.Empty;
        public string DisplayCompanyNameForEdit { get; set; } = string.Empty;

        // Form fields
        [BindProperty]
        public string CompanyId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Name cannot be empty!")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "User ID cannot be empty!")]
        public string UserID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Department cannot be empty!")]
        public string Department { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email cannot be empty!")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format!")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Level must be selected!")]
        public string Level { get; set; }

        [BindProperty]
        public string AccStatus { get; set; } = "Normal";

        [BindProperty]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).{6,}$", ErrorMessage = "Password must be at least 6 characters with letters and numbers!")]
        public string Password { get; set; }

        // Dropdown data
        public List<SelectListItem> CompanyList { get; set; } = new List<SelectListItem>();

        // State flags
        public bool IsLevel2Readonly { get; set; } = false;
        public bool IsAccStatusReadonly { get; set; } = false;
        public bool ShowResetPassword { get; set; } = false;

        // Audit fields
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }

        public override void BindData() { }

        public override IActionResult OnGet()
        {
            ParseQueryString();
            SetupUserState();

            if (Action == EnumAction.View || Action == EnumAction.Delete)
            {
                LoadDisplayData();
            }
            else if (Action == EnumAction.Edit)
            {
                LoadEditData();
                LoadCompanyDropdown();
            }
            else if (Action == EnumAction.Add)
            {
                LoadCompanyDropdown();
            }

            return Page();
        }

        private void SetupUserState()
        {
            string ulevel = SessionGet("ULEVEL") ?? "";
            IsLevel2Readonly = ulevel == "2";
            IsAccStatusReadonly = ulevel != "1";
            ShowResetPassword = ulevel == "1";
        }

        private void LoadCompanyDropdown()
        {
            DataTable dt = Library.Database.BLL.user.GetDLLData();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CompanyList.Add(new SelectListItem
                    {
                        Value = row["COMPANYCODE"].ToString(),
                        Text = row["COMPANYNAME"].ToString()
                    });
                }
            }
        }

        private string GetLevelText(string level)
        {
            switch (level)
            {
                case "1": return "System Administrator";
                case "2": return "User";
                case "3": return "Vendor";
                default: return level;
            }
        }

        private string GetAccStatusText(string status)
        {
            return status == "Locked" ? "Locked" : "Normal";
        }

        private void LoadDisplayData()
        {
            DataTable dt = Library.Database.BLL.user.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                DisplayCompanyName = dt.Rows[0]["COMPANYNAME"].ToString();
                DisplayName = dt.Rows[0]["NAME"].ToString();
                DisplayUserID = dt.Rows[0]["USERID"].ToString();
                DisplayDepartment = dt.Rows[0]["DEPARTMENT"].ToString();
                DisplayEmail = dt.Rows[0]["EMAIL"].ToString();
                DisplayLevel = GetLevelText(dt.Rows[0]["ULEVEL"].ToString());
                DisplayAccStatus = GetAccStatusText(dt.Rows[0]["ACCSTATUS"].ToString());
                CreatedBy = dt.Rows[0]["CREATED_BY"].ToString();
                CreatedDate = dt.Rows[0]["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = dt.Rows[0]["UPDATED_BY"].ToString();
                UpdatedDate = dt.Rows[0]["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        private void LoadEditData()
        {
            DataTable dt = Library.Database.BLL.user.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyId = dt.Rows[0]["COMPANYCODE"].ToString();
                DisplayCompanyNameForEdit = dt.Rows[0]["COMPANYNAME"].ToString();
                Name = dt.Rows[0]["NAME"].ToString();
                UserID = dt.Rows[0]["USERID"].ToString();
                Department = dt.Rows[0]["DEPARTMENT"].ToString();
                Email = dt.Rows[0]["EMAIL"].ToString();
                Level = dt.Rows[0]["ULEVEL"].ToString();
                AccStatus = dt.Rows[0]["ACCSTATUS"].ToString();
                CreatedBy = dt.Rows[0]["CREATED_BY"].ToString();
                CreatedDate = dt.Rows[0]["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = dt.Rows[0]["UPDATED_BY"].ToString();
                UpdatedDate = dt.Rows[0]["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        public IActionResult OnPostSubmit()
        {
            ParseQueryString();
            SetupUserState();

            if (Action == EnumAction.Add && string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password", "Password cannot be empty!");
            }

            if (!ModelState.IsValid)
            {
                LoadCompanyDropdown();
                if (Action == EnumAction.View || Action == EnumAction.Delete) LoadDisplayData();
                return Page();
            }

            string level1 = Level == "1" ? "1" : "0";
            string level2 = Level == "2" ? "1" : "0";
            string level3 = Level == "3" ? "1" : "0";
            string encryptedPassword = !string.IsNullOrEmpty(Password)
                ? Library.Root.Other.BusinessLogicBase.Encrypt(Password)
                : "";

            string result = "0";
            if (Action == EnumAction.Edit)
                result = Library.Database.BLL.user.Maint(Key, CompanyId, Name, UserID, Department, Email, level1, level2, level3, encryptedPassword, AccStatus, (int)EnumAction.Edit);
            else if (Action == EnumAction.Add)
                result = Library.Database.BLL.user.Maint("0", CompanyId, Name, UserID, Department, Email, level1, level2, level3, encryptedPassword, AccStatus, (int)EnumAction.Add);

            if (result == "1")
                return Redirect(GetUrl(EnumAction.None));

            RegisterStartupScript("alert('" + (result == "0" ? "Operation failed" : result.Replace("'", "\\'")) + "');");
            LoadCompanyDropdown();
            if (Action == EnumAction.Edit) LoadEditData();
            return Page();
        }

        public IActionResult OnPostDelete()
        {
            ParseQueryString();
            LoadDisplayData();

            string result = Library.Database.BLL.user.Maint(Key, "", DisplayName, DisplayUserID, DisplayDepartment, DisplayEmail, "", "", "", "", DisplayAccStatus, (int)EnumAction.Delete);
            if (result == "1")
                return Redirect(GetUrl(EnumAction.None));

            RegisterStartupScript("alert('" + (result == "0" ? "Delete failed" : result.Replace("'", "\\'")) + "');");
            return Page();
        }

        public IActionResult OnPostResetPassword()
        {
            ParseQueryString();
            SetupUserState();

            string result = Library.Database.BLL.user.ResetPass(Key);
            if (result == "1")
            {
                RegisterStartupScript("alert('Password has been reset successfully.');");
            }
            else
            {
                RegisterStartupScript("alert('" + (result == "0" ? "Reset password failed" : result.Replace("'", "\\'")) + "');");
            }

            LoadEditData();
            LoadCompanyDropdown();
            return Page();
        }

        public IActionResult OnPostReset()
        {
            return Redirect(Request.Path + Request.QueryString);
        }
    }
}
