using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_COMPANY_DtlModel : BasePageModel
    {
        public MM_COMPANY_DtlModel()
        {
            SetupKey = "MM_COMPANY";
            FunctionControl = false;
        }

        // Display fields
        public string DisplayCompanyCode { get; set; } = string.Empty;
        public string DisplayCompanyName { get; set; } = string.Empty;
        public string DisplayAddress { get; set; } = string.Empty;
        public string DisplayTelephone { get; set; } = string.Empty;
        public string DisplayEmail { get; set; } = string.Empty;
        public string DisplaySlitCode { get; set; } = string.Empty;

        // Form fields
        [BindProperty]
        [Required(ErrorMessage = "Company Code cannot be empty!")]
        public string CompCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Company Name cannot be empty!")]
        public string CompName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Address cannot be empty!")]
        public string Address { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Telephone cannot be empty!")]
        public string Telephone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email cannot be empty!")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format!")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Slit Code cannot be empty!")]
        public string SlitCode { get; set; }

        // Audit fields
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }

        public override void BindData() { }

        public override IActionResult OnGet()
        {
            ParseQueryString();

            if (Action == EnumAction.View || Action == EnumAction.Delete)
            {
                LoadDisplayData();
            }
            else if (Action == EnumAction.Edit)
            {
                LoadEditData();
            }

            return Page();
        }

        private void LoadDisplayData()
        {
            DataTable dt = Library.Database.BLL.Company.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                DisplayCompanyCode = dt.Rows[0]["COMPANYCODE"].ToString();
                DisplayCompanyName = dt.Rows[0]["COMPANYNAME"].ToString().ToUpper();
                DisplayAddress = dt.Rows[0]["ADDRESS"].ToString().ToUpper();
                DisplayTelephone = dt.Rows[0]["TELEPHONE"].ToString();
                DisplayEmail = dt.Rows[0]["EMAIL"].ToString();
                DisplaySlitCode = dt.Rows[0]["SLIT_CODE"].ToString();
                CreatedBy = dt.Rows[0]["CREATED_BY"].ToString();
                CreatedDate = dt.Rows[0]["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = dt.Rows[0]["UPDATED_BY"].ToString();
                UpdatedDate = dt.Rows[0]["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        private void LoadEditData()
        {
            DataTable dt = Library.Database.BLL.Company.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompCode = dt.Rows[0]["COMPANYCODE"].ToString();
                CompName = dt.Rows[0]["COMPANYNAME"].ToString();
                Address = dt.Rows[0]["ADDRESS"].ToString();
                Telephone = dt.Rows[0]["TELEPHONE"].ToString();
                Email = dt.Rows[0]["EMAIL"].ToString();
                SlitCode = dt.Rows[0]["SLIT_CODE"].ToString();
                CreatedBy = dt.Rows[0]["CREATED_BY"].ToString();
                CreatedDate = dt.Rows[0]["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = dt.Rows[0]["UPDATED_BY"].ToString();
                UpdatedDate = dt.Rows[0]["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        public IActionResult OnPostSubmit()
        {
            ParseQueryString();
            if (!ModelState.IsValid)
            {
                if (Action == EnumAction.View || Action == EnumAction.Delete) LoadDisplayData();
                return Page();
            }

            string userID = SessionGet("gstrUserID") ?? "";
            string userHost = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            string result = "0";
            if (Action == EnumAction.Edit)
                result = Library.Database.BLL.Company.Maint(Key, CompCode, CompName.ToUpper(), SlitCode, Address.ToUpper(), Telephone, Email, ((int)EnumAction.Edit).ToString(), userID, userHost);
            else if (Action == EnumAction.Add)
                result = Library.Database.BLL.Company.Maint("0", CompCode, CompName.ToUpper(), SlitCode, Address.ToUpper(), Telephone, Email, ((int)EnumAction.Add).ToString(), userID, userHost);

            if (result == "1")
                return Redirect(GetUrl(EnumAction.None));

            RegisterStartupScript("alert('" + (result == "0" ? "Operation failed" : result.Replace("'", "\\'")) + "');");
            if (Action == EnumAction.Edit) LoadEditData();
            return Page();
        }

        public IActionResult OnPostDelete()
        {
            ParseQueryString();
            LoadDisplayData();

            string userID = SessionGet("gstrUserID") ?? "";
            string userHost = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            string result = Library.Database.BLL.Company.Maint(Key, DisplayCompanyCode, DisplayCompanyName, DisplaySlitCode, DisplayAddress, DisplayTelephone, DisplayEmail, ((int)EnumAction.Delete).ToString(), userID, userHost);
            if (result == "1")
                return Redirect(GetUrl(EnumAction.None));

            RegisterStartupScript("alert('" + (result == "0" ? "Delete failed" : result.Replace("'", "\\'")) + "');");
            return Page();
        }

        public IActionResult OnPostReset()
        {
            ParseQueryString();
            // Redirect back to the same detail page with the correct action/id
            if (Action == EnumAction.Edit)
                return Redirect(GetUrl(EnumAction.Edit));
            else if (Action == EnumAction.Add)
                return Redirect(GetUrl(EnumAction.Add));
            return Redirect(Request.Path + Request.QueryString);
        }
    }
}
